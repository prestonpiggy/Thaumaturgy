using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    internal class MeleeAttack : AbilityState {

        public enum Shape { Circle, Cone, Box }

        [Title ("Specific")]
        public Shape AttackShape;
        public float Range;

        [HideIf ("AttackShape", Shape.Circle)]
        public float Angle;

        public override void OnState (IAbilityUser user, ref AbilityInfo info) {
            
        }

        void ScanCircle () {

        }

        void ScanCone () {

        }

        void ScanBox () {

        }

    }

}