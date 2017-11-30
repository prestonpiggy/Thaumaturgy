using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

using TurkeyWork.Events;

namespace TurkeyWork.World {

    [CreateAssetMenu (menuName = "TurkeyWork/World/Level Map")]
    [HideMonoScript]
    public class WordLevelLayout : SerializedScriptableObject {

        public static WordLevelLayout Instance { get; private set; }

        [HideInInspector, SerializeField] private bool active;

        [AssetsOnly]
        public GameEvent OnLoadingStarted;
        [AssetsOnly]
        public GameEvent OnLevelLoaded;

        [OdinSerialize, InfoBox ("Active", "active")]
        [SerializeField] Dictionary<string, LevelInfo> LevelLoadMap;

        public static void LoadLevelWithKey (string loadKey) {
            if (BoltNetwork.isRunning) {
                if (BoltNetwork.isServer) {
                    LevelInfo levelInfo;
                    if (!Instance.LevelLoadMap.TryGetValue (loadKey, out levelInfo)) {
                        Debug.LogError ($"[WordLevelLayout]: Provided LevelKey not found. ({loadKey})");
                        return;
                    }
                    Instance.OnLoadingStarted?.Raise ();
                    if (levelInfo.LoadingPrefab != null) {
                        var loadingGO = Instantiate (levelInfo.LoadingPrefab);
                        DontDestroyOnLoad (loadingGO);
                        //levelInfo.LoadingPrefab.StartCoroutine (); 
                    }
                    BoltNetwork.LoadScene (levelInfo.Name);
                    Instance.OnLevelLoaded?.Raise ();
                }
            }
            else {
                Debug.Log ("[WordLevelLayout]: Non-Network Scene loading not implemented.");
            }
        }

        [RuntimeInitializeOnLoadMethod (RuntimeInitializeLoadType.AfterSceneLoad)]
        static void RuntimeInitialize () {
            Instance = FindActive ();
        }

        static WordLevelLayout FindActive () {
            var all = Resources.LoadAll<WordLevelLayout> ("");
            foreach (var a in all)
                if (a.active)
                    return a;
            throw new System.NullReferenceException ("WorldLevelLayout reference not found!");
        }

        [System.Serializable]
        struct LevelInfo {
            public string Name;
            public bool IsLevel;
            [AssetsOnly]
            public Management.LoadingScreen LoadingPrefab;
        }

#if UNITY_EDITOR
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
#endif

    }

}
