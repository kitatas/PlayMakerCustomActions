namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Get the Right Vector of the Transform")]
    public class GetTransformRight : FsmStateAction
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
            DoGetRight();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetRight();
        }

        private void DoGetRight()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                return;
            }

            var direction = go.transform.right;

            vector.Value = direction;
            x.Value = direction.x;
            y.Value = direction.y;
            z.Value = direction.z;
        }
    }
}