using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public class ActorBody : MonoBehaviour {

        public IActorMotor Motor;

        [SerializeField] ActorBounds boundingVolume = new ActorBounds (
            new Vector2(-.5f, 2),
            new Vector2(.5f, 2),
            new Vector2(.5f, 0),
            new Vector2(-.5f, 0)
            );
        ActorBounds actorBounds;
        public ActorBounds Bounds => actorBounds;

        private void Awake () {
            UpdateBounds ();
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
