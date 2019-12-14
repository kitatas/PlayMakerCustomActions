namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Sets the TMP value of a TMP Text component.")]
    public class TextMeshProSetText : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with the TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [UIHint(UIHint.TextArea)]
        [Tooltip("The text of the TextMeshProUGUI component.")]
        public FsmString text;

        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        [Tooltip("Repeats every frame")] 
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;
        private string originalString;

        public override void Reset()
        {
            gameObject = null;
            text = null;
            resetOnExit = null;
            everyFrame = false;
        }

        public override void OnEnter()
        {
            var go = Fsm.GetOwnerDefaultTarget(gameObject);
            if (UpdateCache(go))
            {
                tmpText = cachedComponent;
            }

            originalString = tmpText.text;

            DoSetTextValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetTextValue();
        }

        private void DoSetTextValue()
        {
            if (tmpText == null) return;

            tmpText.text = text.Value;
        }

        public override void OnExit()
        {
            if (tmpText == null) return;

            if (resetOnExit.Value)
            {
                tmpText.text = originalString;
            }
        }

#if UNITY_EDITOR
        public override string AutoName()
        {
            return "UISetText : " + ActionHelpers.GetValueLabel(text);
        }
#endif
    }
}