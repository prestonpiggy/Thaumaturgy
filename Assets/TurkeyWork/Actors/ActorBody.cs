using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public class ActorBody : MonoBehaviour {

        [SerializeField] ActorBounds boundingVolume = new ActorBounds (
            new Vector2 (0, 1), new Vector2 (1, 2)
            );
        public ActorBounds Bounds => boundingVolume;

        private void Awake () {
            UpdateBounds ();
        }

        public void UpdateBounds () {
            boundingVolume.SetOrigin (transform.position);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected () {
            UpdateBounds ();
            boundingVolume.DrawSceneGizmos ();
        }
#endif
    }

}
