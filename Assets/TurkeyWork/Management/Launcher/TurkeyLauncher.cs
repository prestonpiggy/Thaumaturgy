using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TurkeyWork.Players;
using TurkeyWork.Events;

namespace TurkeyWork.Management {

    public sealed class TurkeyLauncher : SingletonBehaviour<TurkeyLauncher> {

        public TurkeySettings Settings { get; private set; }
        [SerializeField, HideInInspector] private TurkeySettings defaultSettings;
        private string settingsDataPath;

        private void Awake () {
            settingsDataPath = Path.Combine (Application.persistentDataPath, "LauncherSettings.json");
        }

        public void SaveSettings () {
            if (Settings == null)
                return;
            var data = JsonUtility.ToJson (Settings);
            File.WriteAllText (settingsDataPath, data);
        }

        public void ReloadSettings () {
            string data;
            if (!File.Exists (settingsDataPath)) {
                data = JsonUtility.ToJson (defaultSettings ?? ScriptableObject.CreateInstance<TurkeySettings> ());
                File.WriteAllText (settingsDataPath, data);
            } else {
                data = File.ReadAllText (settingsDataPath);
            }
            Settings = ScriptableObject.CreateInstance<TurkeySettings> ();
            JsonUtility.FromJsonOverwrite (data, Settings);
        }

        [RuntimeInitializeOnLoadMethod]
        static void InitializeTurkeyGame () {
            var scene = SceneManager.GetSceneByName ("Logic");
            Instance.ReloadSettings ();

            if (scene.IsValid ())
                Debug.Log ("Logic Scene already loaded!");
            else {
                SceneManager.LoadScene ("Logic", LoadSceneMode.Additive);
                Debug.Log ("Loaded logic!");
            }

#if UNITY_EDITOR
            if (Instance.Settings.LoadFromStart)
                SceneManager.LoadScene (0);
#endif
        }
    }

}