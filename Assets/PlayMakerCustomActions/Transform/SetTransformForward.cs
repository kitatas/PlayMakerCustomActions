namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Sets the Forward of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
    public class SetTransformForward : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject to forward.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Use a stored Vector3 forward, and/or set individual axis below.")]
        public FsmVector3 vector;

        public FsmFloat x;
        public FsmFloat y;
        public FsmFloat z;

        [Tooltip("Repeat every frame.")]
        public bool everyFrame;

        [Tooltip("Perform in LateUpdate. This is useful if you want to override the position of objects that are animated or otherwise positioned in Update.")]
        public bool lateUpdate;

        public override void Reset()
        {
            gameObject = null;
            vector = null;
            x = new FsmFloat {UseVariable = true};
            y = new FsmFloat {UseVariable = true};
            z = new FsmFloat {UseVariable = true};
            everyFrame = false;
            lateUpdate = false;
        }

        public override void OnPreprocess()
        {
            if (lateUpdate)
            {
                Fsm.HandleLateUpdate = true;
            }
        }

        public override void OnEnter()
        {
            if (!everyFrame && !lateUpdate)
            {
                DoSetForward();
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (!lateUpdate)
            {
                DoSetForward();
            }
        }

        public override void OnLateUpdate()
        {
            if (lateUpdate)
            {
                DoSetForward();
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        private void DoSetForward()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var forward = vector.IsNone ? go.transform.forward : vector.Value;

            if (!x.IsNone) forward.x = x.Value;
            if (!y.IsNone) forward.y = y.Value;
            if (!z.IsNone) forward.z = z.Value;

            go.transform.forward = forward;
        }
    }
}