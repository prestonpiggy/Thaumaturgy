using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Thauma {

    public class ColorLerp : UIEffect {

        public bool FadeAtStart = true;

        public float Duration = 2f;
        public Color StartColor = new Color (1, 1, 1, 0);
        public Color TargetColor = Color.white;
        public AnimationCurve FadeCurve = AnimationCurve.EaseInOut (0, 0, 1, 1);

        Coroutine fade;
        Image image;

        private void Start () {
            image = GetComponent<Image> ();
            if (FadeAtStart)
                fade = StartCoroutine (DoFade ());
        }

        public override void PlayEffect () {
            if (fade == null)
                fade = StartCoroutine (DoFade ());
        }

        IEnumerator DoFade () {
            if (StartDelay > 0)
                yield return new WaitForSeconds (StartDelay);

            var timeFinished = Time.time + Duration;
            var time = 0f;
            image.color = StartColor;

            while (timeFinished > Time.time) {
                time += Time.deltaTime / Duration;
                image.color = Color.Lerp (StartColor, TargetColor, FadeCurve.Evaluate (time));
                yield return null;
            }
            image.color = TargetColor;
            fade = null;
            this.enabled = false;
            OnEffectPlayed.Invoke ();
        }
    }

}
