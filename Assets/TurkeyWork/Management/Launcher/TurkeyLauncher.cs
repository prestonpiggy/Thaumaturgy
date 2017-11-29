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

        public static PlayerProfile CurrentProfile { get; private set; }

        private void Awake () {
            settingsDataPath = Path.Combine (Application.persistentDataPath, "LauncherSettings.json");
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

        public static bool LoadPlayerProfile (string name) {
            return true;
        }

        public static bool CreateProfile (string name) {
            var path = Path.Combine (Application.persistentDataPath, "Profiles", name);
            if (Directory.Exists (path))
                return false;
            Directory.CreateDirectory (path);
            path = Path.Combine (path, "PlayerProfile.json");
            CurrentProfile = new PlayerProfile (name);

            Instance.Settings.LastProfile = name;

            File.WriteAllText (path, JsonUtility.ToJson (CurrentProfile));
            Debug.Log ($"[TurkeyLauncher]: Player Profile created ({name}).");
            return true;
        }

        public static bool TryLoadLastProfile () {
            if (string.IsNullOrEmpty (Instance.Settings.LastProfile)) {
                Debug.Log ("[TurkeyLauncher]: Could not load last Player Profile.");
                return false;
            }
            var path = Path.Combine (Application.persistentDataPath, "Profiles", Instance.Settings.LastProfile, "PlayerProfile.json");
            var json = File.ReadAllText (path);
            CurrentProfile = JsonUtility.FromJson<PlayerProfile> (json);
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