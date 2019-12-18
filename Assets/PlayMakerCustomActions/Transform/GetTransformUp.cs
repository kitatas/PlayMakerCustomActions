namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory(ActionCategory.Transform)]
    [Tooltip("Get the Up Vector of the Transform")]
    public class GetTransformUp : FsmStateAction
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
            DoGetUp();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetUp();
        }

        private void DoGetUp()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);

            if (go == null)
            {
                return;
            }

            var direction = go.transform.up;

            vector.Value = direction;
            x.Value = direction.x;
            y.Value = direction.y;
            z.Value = direction.z;
        }
    }
}