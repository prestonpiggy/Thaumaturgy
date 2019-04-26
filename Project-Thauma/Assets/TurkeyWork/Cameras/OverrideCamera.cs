using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Cameras {

    public class OverrideCamera : CameraController {

        private void OnEnable () {
            ForceActive ();
        }

        private void OnDisable () {
            if (mainCamera)
                mainCamera.gameObject.SetActive (true);
        }

    }

}