using UnityEngine;

namespace HutongGames.PlayMaker.Actions
{
    [ActionCategory("TextMeshPro")]
    [Tooltip("Set TMP Text Face Color")]
    public class TextMeshProSetFaceColor : ComponentAction<TMPro.TextMeshProUGUI>
    {
        [RequiredField]
        [CheckForComponent(typeof(TMPro.TextMeshProUGUI))]
        [Tooltip("The GameObject with a TextMeshProUGUI component.")]
        public FsmOwnerDefault gameObject;

        [Tooltip("The Color of the TextMeshProUGUI component. Leave to none and set the individual color values, for example to affect just the alpha channel")]
        public FsmColor color;

        [Tooltip("The red channel Color of the TextMeshProUGUI component. Leave to none for no effect, else it overrides the color property")]
        public FsmFloat red;

        [Tooltip("The green channel Color of the TextMeshProUGUI component. Leave to none for no effect, else it overrides the color property")]
        public FsmFloat green;

        [Tooltip("The blue channel Color of the TextMeshProUGUI component. Leave to none for no effect, else it overrides the color property")]
        public FsmFloat blue;

        [Tooltip("The alpha channel Color of the TextMeshProUGUI component. Leave to none for no effect, else it overrides the color property")]
        public FsmFloat alpha;

        [Tooltip("Reset when exiting this state.")]
        public FsmBool resetOnExit;

        [Tooltip("Repeats every frame, useful for animation")]
        public bool everyFrame;

        private TMPro.TextMeshProUGUI tmpText;
        private Color originalColor;

        public override void Reset()
        {
            gameObject = null;
            color = null;

            red = new FsmFloat {UseVariable = true};
            green = new FsmFloat {UseVariable = true};
            blue = new FsmFloat {UseVariable = true};
            alpha = new FsmFloat {UseVariable = true};

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

            originalColor = tmpText.faceColor;

            DoSetFaceColorValue();

            if (!everyFrame)
            {
                Finish();
            }
        }

        public override void OnUpdate()
        {
            DoSetFaceColorValue();
        }

        public override void OnExit()
        {
            if (tmpText == null)
            {
                return;
            }

            if (resetOnExit.Value)
            {
                tmpText.faceColor = originalColor;
            }
        }

        private void DoSetFaceColorValue()
        {
            if (tmpText == null)
            {
                return;
            }

            var col = tmpText.faceColor;

            if (!color.IsNone)
            {
                col = color.Value;
            }

            if (!red.IsNone)
            {
                col.r = (byte) red.Value;
            }

            if (!green.IsNone)
            {
                col.g = (byte) green.Value;
            }

            if (!blue.IsNone)
            {
                col.b = (byte) blue.Value;
            }

            if (!alpha.IsNone)
            {
                col.a = (byte) alpha.Value;
            }

            tmpText.faceColor = col;
        }
    }
}