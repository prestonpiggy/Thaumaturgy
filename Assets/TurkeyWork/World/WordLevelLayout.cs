using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace TurkeyWork.World {

    [CreateAssetMenu (menuName = "TurkeyWork/World/Level Map")]
    public class WordLevelLayout : SerializedScriptableObject {

        public static WordLevelLayout Instance { get; private set; }

        [HideInInspector, SerializeField] private bool active;

        [OdinSerialize, InfoBox ("Active", "active")]
        public Dictionary<string, string> LevelLoadMap;

        public static void LoadLevelWithKey (string loadKey) {
            if (BoltNetwork.isRunning) {
                if (BoltNetwork.isServer) {
                    string level;
                    if (!Instance.LevelLoadMap.TryGetValue (loadKey, out level)) {
                        Debug.LogError ("[WordLevelLayout]: Provided LevelKey not found in Active LevelLayout!");
                    }
                    BoltNetwork.LoadScene (level);
                }
            }
            else {
                Debug.Log ("[WordLevelLayout]: Non-Network Scene loading not implemented.");
            }
        }

        [RuntimeInitializeOnLoadMethod]
        static void RuntimeInitialize () {
            Instance = FindActive ();
        }

        static WordLevelLayout FindActive () {
            var all = Resources.LoadAll<WordLevelLayout> ("");
            foreach (var a in all)
                if (a.active)
                    return a;
            throw new System.NullReferenceException ("World Layout reference not found!");
        }

        [HideIf ("active"), Button ("Set Active")]
        void DisableOthers () {
            var all = Resources.LoadAll<WordLevelLayout> ("");
            foreach (var a in all)
                a.active = false;
            active = true;
        }

        [UnityEditor.MenuItem ("TurkeyWork/Active Level Layout")]
        static void SelectActive () {
            UnityEditor.Selection.activeObject = FindActive ();
        }

    }

}
