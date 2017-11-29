using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TurkeyWork.Management {

    public sealed partial class TurkeySettings : ScriptableObject {

        private string gameVersion = "0.0.0.01a";
        private string lastProfile;

        public string GameVersion => gameVersion;
        public string LastProfile => lastProfile;
    }

#if UNITY_EDITOR
    public sealed partial class TurkeySettings {

        public bool LoadFromStart;

        [MenuItem ("TurkeyWork/Turkey Settings")]
        public static void OpenTurkeySettings () {
            Debug.Log ("asd");
        }

    }
#endif

}
