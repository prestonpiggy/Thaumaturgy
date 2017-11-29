using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

using TurkeyWork.Events;

namespace TurkeyWork.Management {

    public class ProfileLoader : MonoBehaviour {

        public bool LoadAtStart;
        [AssetsOnly]
        public GameEvent PlayerLoadedEvent;
        public string CreateProfileScene;
        
        void Start () {
            if (LoadAtStart) {
                if (TurkeyLauncher.CurrentProfile == null) {
                    if (TurkeyLauncher.TryLoadLastProfile ()) {
                        PlayerLoadedEvent.Raise ();
                    } else {
                        SceneManager.LoadScene (CreateProfileScene, LoadSceneMode.Additive);
                    }
                }
                enabled = false;
            }
        }

    }

}
