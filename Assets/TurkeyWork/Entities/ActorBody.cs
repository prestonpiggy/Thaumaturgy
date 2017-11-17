using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public class ActorBody : MonoBehaviour {

        public IActorMotor Motor;

        [SerializeField] ActorBounds boundingVolume;
        ActorBounds actorBounds;
        public ActorBounds Bounds => actorBounds;

        private void Awake () {
            Motor = GetComponent<IActorMotor> ();
        }

        private void Update () {
            UpdateBounds ();
        }

        public void UpdateBounds () {
            actorBounds = boundingVolume.CenterOnPosition (transform.position);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected () {
            UpdateBounds ();
            actorBounds.DrawSceneGizmos ();
        }
#endif
    }

}
