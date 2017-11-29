using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

using TurkeyWork.Players;

namespace TurkeyWork.Management {

    public class ProfileManager : SingletonBehaviour<ProfileManager> {

        public static PlayerProfile CurrentProfile { get; private set; }

        public static string CurrentProfileDirectory {
            get {
                if (CurrentProfile == null)
                    return null;
                return Path.Combine (Application.persistentDataPath, "Profiles", CurrentProfile.Name);
            }
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

            TurkeyLauncher.Instance.Settings.LastProfile = name;
            TurkeyLauncher.Instance.SaveSettings ();
            
            File.WriteAllText (path, JsonUtility.ToJson (CurrentProfile));
            Debug.Log ($"[{nameof (ProfileManager)}]: Player Profile created ({name}).");
            return true;
        }

        public static bool TryLoadLastProfile () {
            if (string.IsNullOrEmpty (TurkeyLauncher.Instance.Settings.LastProfile)) {
                Debug.Log ($"[{nameof (ProfileManager)}]: Could not load last Player Profile.");
                return false;
            }
            var path = Path.Combine (
                Application.persistentDataPath,
                "Profiles",
                TurkeyLauncher.Instance.Settings.LastProfile,
                "PlayerProfile.json"
                );
            var json = File.ReadAllText (path);
            CurrentProfile = JsonUtility.FromJson<PlayerProfile> (json);
            return true;
        }

    }

}