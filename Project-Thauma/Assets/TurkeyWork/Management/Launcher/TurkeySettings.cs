using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TurkeyWork.Management {

    public sealed partial class TurkeySettings : ScriptableObject {

        public string GameVersion = "0.0.0.01a";
        public string LastProfile;
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
