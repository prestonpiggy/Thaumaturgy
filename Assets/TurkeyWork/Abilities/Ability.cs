using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Actors;

namespace TurkeyWork.Abilities {

    public abstract class Ability : ScriptableObject {

        public float MovementSpeedMultiplier = 1;
        public float SpeedMultiplierDuration = 1;

        public abstract IEnumerator<AbilityInfo> Use (Player player);

        protected void LogStart (Player player) {
            Debug.Log ($"{player.name}: Started using an ability. ({Time.time})");          
        }

        protected void LogFinish (Player player) {
            Debug.Log ($"{player.name}:Finished using an ability. ({Time.time})");
        }

        // this should probably be something othre than ActorBody
        protected List<ActorBody> DamageHitTargets (RaycastHit2D[] hits) {
            var hitActors = new List<ActorBody> ();
            foreach (var hit in hits) {
                var actor = hit.transform.GetComponent<ActorBody> ();

                if (actor != null) {
                    hitActors.Add (actor);
                    Debug.Log (actor.name);
                }
            }
            return hitActors;
        }

        protected List<ActorBody> DamageHitTargets (Collider2D[] hitColliders) {
            Debug.Log (hitColliders.Length);
            var hitActors = new List<ActorBody> ();
            foreach (var hit in hitColliders) {
                var actor = hit.transform.GetComponent<ActorBody> ();

                if (actor != null) {
                    hitActors.Add (actor);
                    Debug.Log (actor.name);
                }
            }
            return hitActors;
        }
    }

}