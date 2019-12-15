namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Get TMP Text Face Color.")]
    public class TextMeshProGetFaceColor : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with the TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The Color of the TextMeshProUGUI component")]
        public FsmColor color;

        [Tooltip("Repeats every frame")] 
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;

        public override void Reset()
        {
            gameObject = null;
            color = null;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                tmpText = cachedComponent;
            }

            DoGetColorValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetColorValue();
        }

        private void DoGetColorValue()
        {
            if (tmpText != null)
            {
                color.Value = tmpText.faceColor;
            }
        }
    }
}