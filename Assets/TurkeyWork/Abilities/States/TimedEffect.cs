using System.Collections;
using System.Collections.Generic;
using TurkeyWork.Actors;
using UnityEngine;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public class TimedEffect : AbilityState {

        enum TargetType { Self, HitEnemies }

        public override void ResolveState (ActorBody actor, ref AbilityInfo abilityInfo) {
            
        }
    }

}