using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TurkeyWork.Players;
namespace TurkeyWork.Launcher {

    public sealed class TurkeyLauncher : SingletonBehaviour<TurkeyLauncher> {

        public event System.Action PlayerProfileLoaded;
        public event System.Action<PlayerProfile> PlayerProfileUpdated;

        public TurkeySettings Settings { get; private set; }
        [SerializeField, HideInInspector] private TurkeySettings defaultSettings;
        private string settingsDataPath;

        public PlayerProfile CurrentProfile { get; private set; }

        public string currentProfile;

        private void Awake () {
            settingsDataPath = Path.Combine (Application.persistentDataPath, "settings.json");
        }

        private void Start () {
            // TEMP!
            LoadProfile (Settings.LastProfile);
        }

        public void ReloadSettings () {
            print (settingsDataPath);
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

        public void LoadProfile (string profileName) {
            CurrentProfile = new PlayerProfile ();
            // Else just load the profile.
            PlayerProfileUpdated?.Invoke (CurrentProfile);
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