using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "TurkeyWork/Skill")]
    public class Skill : SerializedScriptableObject {

        [HideIf ("hasCost")]
        [SerializeField] bool hasCost;

        [HideLabel, ShowIf ("hasCost"), Header ("Resource Cost"), CustomContextMenu ("Remove", "RemoveCost")]
        [SerializeField] AbilityCost cost;

        [OdinSerialize] AbilityState[] states = new AbilityState[0];

        public bool HasCost => hasCost;

        public bool Use (IAbilityUser user, ref AbilityInfo abilityInfo) {
            user.Behaviour.StartCoroutine (UseAbility (abilityInfo));
            return true;
        }

        IEnumerator UseAbility (AbilityInfo abilityInfo) {
            yield return null;
        }

        void OnItrreupted (IAbilityUser info) {

        }

        void RemoveCost () {
            hasCost = false;
        }
        
        [System.Serializable]
        struct AbilityCost {
            public string Stat;
            public int Amount;
        }
    }

}