namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Get the Forward Vector of the Transform")]
    public class GetTransformForward : FsmStateAction
    {
        [RequiredField] public FsmOwnerDefault gameObject;

        [UIHint(UIHint.Variable)] public FsmVector3 vector;
        [UIHint(UIHint.Variable)] public FsmFloat x;
        [UIHint(UIHint.Variable)] public FsmFloat y;
        [UIHint(UIHint.Variable)] public FsmFloat z;

        public bool everyFrame;

        public override void Reset()
        {
            gameObject = null;
            vector = null;
            x = null;
            y = null;
            z = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            DoGetForward();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetForward();
        }

        private void DoGetForward()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                return;
            }

            var direction = go.transform.forward;

            vector.Value = direction;
            x.Value = direction.x;
            y.Value = direction.y;
            z.Value = direction.z;
        }
    }
}