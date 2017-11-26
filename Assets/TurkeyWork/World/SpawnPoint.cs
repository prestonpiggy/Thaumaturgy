using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.World {

    public class SpawnPoint : MonoBehaviour {

        public LayerMask CheckMask;

        public bool CheckSafety () {
            return CheckSafety (3f, true, CheckMask);
        }

        public bool CheckSafety (float radius, bool allowVision) {
            return CheckSafety (radius, allowVision, CheckMask);
        }

        public bool CheckSafety (float radius, bool allowVision, LayerMask checkMask) {
            return Physics.CheckSphere (transform.position, radius, checkMask);
        }

        private void OnEnable () {
            SpawnManager.RegisterSpawnPoint (this);
        }

        private void OnDisable () {
            SpawnManager.UnregisterSpawnPoint (this);
        }

    }

}