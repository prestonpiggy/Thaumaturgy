using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork {

    public abstract class SingletonBehaviour<T> : MonoBehaviour
        where T : SingletonBehaviour<T> {

        private static T instance;
        public static T Instance {
            get {
                if (!instance) {
                    instance = FindObjectOfType<T> ()
                        ?? (instance = new GameObject (typeof (T).GetType ().Name).AddComponent<T> ());
                    DontDestroyOnLoad (instance.gameObject);
                }                
                return instance;
            }
        }
    }

}
