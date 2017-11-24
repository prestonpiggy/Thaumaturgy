using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TurkeyWork.Entities;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "TurkeyWork/Abilties/Ranged Attack")]
    public class ProjectileAttack : Ability {

        public Projectile SpawnedProjectile;

        public override IEnumerator<AbilityInfo> Use (Player player) {
            var wait = new AbilityInfo ();
            Debug.Log ("Projectile Attack");
            yield return wait;
            wait.IsDone = true;
            yield return wait;
        }
    }

}