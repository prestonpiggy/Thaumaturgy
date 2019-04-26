using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TurkeyWork.Management {

    public class LoadingScreen : MonoBehaviour {

        [SerializeField] Text targetLevelText;

        public string TargetLevelName {
            get {
                return targetLevelText.text;
            }
            set {
                targetLevelText.text = value;
            }
        }
    }


}