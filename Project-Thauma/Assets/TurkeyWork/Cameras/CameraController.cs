using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Cameras {

    public abstract class CameraController : MonoBehaviour {

        protected static MainCamera mainCamera;

        [HideIf ("HasCamera")]
        [SerializeField] protected Camera attachedCamera;

        bool HasCamera => attachedCamera != null;

        public void ForceActive () {
            var cams = FindObjectsOfType<CameraController> ();
            foreach (var cam in cams)
                cam.gameObject.SetActive (cam == this);
        }

        [Button]
        protected void PreviewCamera () {
            ForceActive ();
        }

    }

}
