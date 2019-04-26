using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TurkeyWork.Management {

    public static class SaveManager {

        static string profileSavePath = ProfileManager.CurrentProfileDirectory;

        public static void Save<T> (string fileName, T objectToSave) {
            var fileSavePath = GetPath (fileName);
            var directory = GetSaveDirectory ();
            if (!Directory.Exists (directory)) {
                Directory.CreateDirectory (directory);
            }

            var data = JsonUtility.ToJson (objectToSave);
            File.WriteAllText (fileSavePath, data);
        }

        public static bool Load<T> (string fileName, T loadedObject) where T : UnityEngine.Object {
            var json = LoadJson (fileName);
            if (string.IsNullOrEmpty (json)) {
                return false;
            }
            JsonUtility.FromJsonOverwrite (json, loadedObject);
            return true;
        }

        public static bool Load<T> (string fileName, out T loadedObject) {
            var json = LoadJson (fileName);
            if (string.IsNullOrEmpty (json)) {
                loadedObject = default (T);
                return false;
            }
            loadedObject = JsonUtility.FromJson<T> (json);
            return true;
        }

        static string LoadJson (string fileName) {
            var savePath = GetPath (fileName);

            if (!File.Exists (savePath)) {
                return null;
            }

            var content = File.ReadAllText (savePath);
            return content;
        }

        static string GetSaveDirectory () {
            return Path.Combine (profileSavePath, ProfileManager.CurrentProfile.Name);
        }

        static string GetPath (string fileName) {
            return Path.Combine (profileSavePath, ProfileManager.CurrentProfile.Name, fileName); ;
        }
    }

}
