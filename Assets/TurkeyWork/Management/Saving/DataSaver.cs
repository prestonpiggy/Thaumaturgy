using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Management {

    public class DataSaver : MonoBehaviour {

        [ReadOnly, ShowInInspector] ISaveHandler[] saveHandlers;

        private void Awake () {
            saveHandlers = GetComponents<ISaveHandler> ();
        }
    }

}