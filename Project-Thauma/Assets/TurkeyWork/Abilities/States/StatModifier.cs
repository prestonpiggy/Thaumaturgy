using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    internal class StatModifier : AbilityState {

        [Title ("Specific")]
        public string TargetStat;
        public int Flat;
        public float Mult;

        public override void OnState (IAbilityUser user, ref AbilityInfo info) {

        }

    }
}
