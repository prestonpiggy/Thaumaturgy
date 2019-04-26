using UnityEngine;

namespace TurkeyWork.HUD{
    [CreateAssetMenu(menuName = "HUD/IndicatorTemplate")]
    public class AttributeIndicatorTemplate : ScriptableObject
    {

        public Color[] colorGradients;
        public Sprite background, foreground;
        public AnimationCurve animationCurve;
    }
}

