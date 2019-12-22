namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Sets the Up of a Game Object. To leave any axis unchanged, set variable to 'None'.")]
    public class SetTransformUp : FsmStateAction
    {
        [RequiredField]
        [Tooltip("The GameObject to up.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)]
        [Tooltip("Use a stored Vector3 up, and/or set individual axis below.")]
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
                DoSetUp();
                Finish();
            }
        }

        public override void OnUpdate()
        {
            if (!lateUpdate)
            {
                DoSetUp();
            }
        }

        public override void OnLateUpdate()
        {
            if (lateUpdate)
            {
                DoSetUp();
            }

            if (!everyFrame)
            {
                Finish();
            }
        }

        private void DoSetUp()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (go == null)
            {
                return;
            }

            var up = vector.IsNone ? go.transform.up : vector.Value;

            if (!x.IsNone) up.x = x.Value;
            if (!y.IsNone) up.y = y.Value;
            if (!z.IsNone) up.z = z.Value;

            go.transform.up = up;
        }
    }
}