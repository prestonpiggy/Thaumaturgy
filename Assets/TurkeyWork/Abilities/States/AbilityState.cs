using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Actors;

namespace TurkeyWork.Abilities {

    public abstract class AbilityState : ScriptableObject {

        public abstract void ResolveState (ActorBody actor, ref AbilityInfo abilityInfo);

    }

}
