using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "TurkeyWork/Abilities/Skill")]
    [HideMonoScript]
    public class Skill : SerializedScriptableObject {

        [HideIf ("hasCost")]
        [SerializeField] bool hasCost;

        [HideLabel, ShowIf ("hasCost"), Header ("Resource Cost"), CustomContextMenu ("Remove", "EDITOR_RemoveCost")]
        [SerializeField] StatCost cost;

        [OdinSerialize] AbilityState[] states = new AbilityState[0];

        public bool HasCost => hasCost;

        public bool Use (IAbilityUser user, ref AbilityInfo abilityInfo) {
            if (hasCost && !user.DeductStat (cost))
                return false;

            user.Behaviour.StartCoroutine (UseAbility (abilityInfo));
            return true;
        }

        IEnumerator UseAbility (AbilityInfo abilityInfo) {
            yield return null;
        }

        void OnItrreupted (IAbilityUser info) {

        }

#if UNITY_EDITOR
        void EDITOR_RemoveCost () {
            hasCost = false;
        }
#endif
    }

}