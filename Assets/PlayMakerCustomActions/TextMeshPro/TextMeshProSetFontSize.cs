namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Sets the TMP Font Size.")]
    public class TextMeshProSetFontSize : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with the TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [RequiredField]
        [UIHint(UIHint.FsmFloat)]
        [Tooltip("The font size of the TextMeshProUGUI component.")]
        public FsmFloat size;

        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        [Tooltip("Runs every frame. Useful to animate values over time.")]
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;
        private float originSize;

        public override void Reset()
        {
            gameObject = null;
            size = null;
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

            originSize = tmpText.fontSize;

            DoSetFontSize();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetFontSize();
        }

        public override void OnExit()
        {
            if (tmpText == null)
            {
                return;
            }

            if (resetOnExit.Value)
            {
                tmpText.fontSize = originSize;
            }
        }

        private void DoSetFontSize()
        {
            if (tmpText == null)
            {
                return;
            }

            if (size.Value > 0)
            {
                tmpText.fontSize = size.Value;
            }
        }
    }
}