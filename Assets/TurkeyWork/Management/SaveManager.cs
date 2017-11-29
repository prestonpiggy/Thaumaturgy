using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TurkeyWork.Management {

    public static class SaveSystem {

        static string SAVE_PATH = Path.Combine (Application.persistentDataPath, "Profiles");

        public static void Save<T> (string fileName, T objectToSave) {
            var savePath = GetPath (fileName);
            var data = JsonUtility.ToJson (objectToSave);
            File.WriteAllText (SAVE_PATH, data);
        }

        public static bool Load<T> (string fileName, out T loadedObject) {
            var savePath = GetPath (fileName);

            if (!File.Exists (savePath)) {
                loadedObject = default(T);
                return false;
            }

            var content = File.ReadAllText (savePath);
            loadedObject = JsonUtility.FromJson<T> (content);
            return true;
        }

        static string GetPath (string fileName) {
            return Path.Combine (SAVE_PATH, ProfileManager.CurrentProfile.Name, fileName); ;
        }
    }

}
