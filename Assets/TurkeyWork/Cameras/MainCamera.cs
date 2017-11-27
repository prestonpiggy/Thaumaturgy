using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

namespace TurkeyWork.Cameras {

    [HideMonoScript]
    public class MainCamera : CameraController {

        // Use this for initialization
        void Awake () {
            mainCamera = this;
        }

    }

}
