using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    internal class HitScanAttack : AbilityState {

        public enum HitScanType { Line, Circle }

        public HitScanType scanType;
        public float Range;

        public override void OnState (IAbilityUser user, ref AbilityInfo info) {

        }

    }

}