using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TurkeyWork.Players;
using TurkeyWork.Events;

namespace TurkeyWork.Management {

    public sealed class TurkeyLauncher : SingletonBehaviour<TurkeyLauncher> {

        public TurkeySettings Settings { get; private set; }
        [SerializeField, HideInInspector] private TurkeySettings defaultSettings;
        private string settingsDataPath;

        public static PlayerProfile CurrentProfile { get; private set; }

        private void Awake () {
            settingsDataPath = Path.Combine (Application.persistentDataPath, "settings.json");
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

        public static bool TryLoadLastProfile () {
            print (Instance.Settings.LastProfile);
            if (string.IsNullOrEmpty (Instance.Settings.LastProfile))
                return false;
            CurrentProfile = new PlayerProfile (Instance.Settings.LastProfile);
            return true;
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