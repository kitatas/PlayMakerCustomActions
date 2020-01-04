namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Gets the TMP value of a TMP Text component.")]
    public class TextMeshProGetText : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with the TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.Variable)]
        [Tooltip("The text value of the TextMeshProUGUI component.")]
        public FsmString text;

        [Tooltip("Runs every frame. Useful to animate values over time.")]
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;

        public override void Reset()
        {
            gameObject = null;
            text = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                tmpText = cachedComponent;
            }

            DoGetTextValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoGetTextValue();
        }

        private void DoGetTextValue()
        {
            if (tmpText != null)
            {
                text.Value = tmpText.text;
            }
        }
    }
}