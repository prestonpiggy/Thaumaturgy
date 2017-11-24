using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Actors;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public abstract class AbilityState {

        public bool SetAnimation;

        [ShowIf ("SetAnimation")]
        public string AnimationName;

        public float WaitAfter;

        public abstract void ResolveState (ActorBody actor, ref AbilityInfo abilityInfo);

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
