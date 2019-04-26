using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Thauma {

    public class ColorLerp : UIEffect {

        public float Duration = 2f;
        public Gradient Color;

        Coroutine fade;
        Image image;

        private void Start () {
            image = GetComponent<Image> ();
            if (PlayAtStart)
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
            image.color = Color.Evaluate (0);

            while (timeFinished > Time.time) {
                time += Time.deltaTime / Duration;
                image.color = Color.Evaluate (time);
                yield return null;
            }
            image.color = Color.Evaluate (1);
            fade = null;
            this.enabled = false;
            OnEffectPlayed.Invoke ();
        }
    }

}
