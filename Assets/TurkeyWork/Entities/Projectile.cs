using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Actors;

namespace TurkeyWork.Entities {

    public class Projectile : MonoBehaviour {

        public float Speed;
        ActorBody owner;

        public Projectile Create (ActorBody player, Vector3 position, Quaternion rotation) {
            var p = Instantiate (this, position, rotation);
            p.owner = player;
            return p;
        }

        void OnTriggerEnter (Collider other) {
            if (other.gameObject.GetInstanceID () == owner.gameObject.GetInstanceID ())
                return;
            Debug.Log ($"Projectile Hit {other.name}");
            Destroy (this);
        }
    }

}