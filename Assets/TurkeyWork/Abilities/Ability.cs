using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    public class Ability : ScriptableObject {

        public AbilityInfo Use (MonoBehaviour user) {
            var abilityInfo = new AbilityInfo () {
                User = user
            };

            user.StartCoroutine (UseAbility (abilityInfo));
            return abilityInfo;
        }

        IEnumerator UseAbility (AbilityInfo abilityInfo) {
            yield return null;
        }

        void OnItrreupted (AbilityInfo info) {

        }
        
    }

}