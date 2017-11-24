using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TurkeyWork.Stats;
using TurkeyWork.Actors;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public class AdjustStats : AbilityState {

        public enum ModifierType { Flat, Multiplier }

        public string StatName;
        public ModifierType Type;

        [ShowIf ("Type", ModifierType.Flat)]
        public int FlatAmount;
        [ShowIf ("Type", ModifierType.Multiplier)]
        public float Multiplier;

        public override void ResolveState (ActorBody actor, ref AbilityInfo abilityInfo) {
            
        }
    }

}