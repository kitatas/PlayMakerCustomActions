namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Gets the TMP Font Size.")]
    public class TextMeshProGetFontSize : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with the TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The font size of the TextMeshProUGUI component.")]
        public FsmFloat size;

        [Tooltip("Runs every frame. Useful to animate values over time.")]
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;

        public override void Reset()
        {
            gameObject = null;
            size = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                tmpText = cachedComponent;
            }

            DoGetFontSize();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetFontSize();
        }

        private void DoGetFontSize()
        {
            if (tmpText != null)
            {
                size.Value = tmpText.fontSize;
            }
        }
    }
}