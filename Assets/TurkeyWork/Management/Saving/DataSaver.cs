using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Management {

    public class DataSaver : MonoBehaviour {

        public enum LoadSaveMode { Manual, OnDisable, OnEnable, OnAwake, OnDestroy }

        public LoadSaveMode saveMode = LoadSaveMode.OnDisable;
        public LoadSaveMode loadMode = LoadSaveMode.OnEnable;
    
        [ReadOnly, ShowInInspector] ISaveHandler[] saveHandlers;

        public void SaveAll () {
            Debug.Log ($"[DataSaver ({name}): Saving started. ({Time.time})]");
            foreach (var savable in saveHandlers) {
                savable.OnSaveData ();
            }
            Debug.Log ($"[DataSaver ({name}): Saving completed. ({Time.time})]");
        }

        public void LoadAll () {
            Debug.Log ($"[DataSaver ({name}): Loading started. ({Time.time})]");
            foreach (var savable in saveHandlers) {
                savable.OnLoadData ();
            }
            Debug.Log ($"[DataSaver ({name}): Loading completed. ({Time.time})]");
        }

        private void Awake () {
            saveHandlers = GetComponents<ISaveHandler> ();

            if (loadMode == LoadSaveMode.OnAwake) {
                LoadAll ();
            }
        }

        private void OnEnable () {
            if (saveMode == LoadSaveMode.OnEnable) {
                SaveAll ();
            }
        }

        private void OnDisable () {
            if (loadMode == LoadSaveMode.OnDisable) {
                LoadAll ();
            }    
        }

        private void OnDestroy () {
            if (saveMode == LoadSaveMode.OnDestroy) {
                SaveAll ();
            }
        }
    }

}