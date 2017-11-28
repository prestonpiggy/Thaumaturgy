using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


namespace Game {

    [HideMonoScript]
    public class DontDestroyOnLoad : MonoBehaviour {

        void Awake () {
            DontDestroyOnLoad (this.gameObject);
        }

    }

}