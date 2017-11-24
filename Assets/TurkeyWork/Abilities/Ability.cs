using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TurkeyWork.Actors;
using System.Linq;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "TurkeyWork/Ability/Modular Ability")]
    [ShowOdinSerializedPropertiesInInspector]
    public class Ability : SerializedScriptableObject {

        [OdinSerialize] AbilityState[] States;

        public virtual IEnumerator<AbilityInfo> Use (Player player) {
            var abilityInfo = new AbilityInfo ();

            yield return abilityInfo;
        }

        protected int abilityID;

        private void OnEnable () {
            abilityID = GetInstanceID ();
        }

        protected void LogStart (Player player) {
            Debug.Log ($"{player.name}: Started using an ability. ({Time.time})");          
        }

        protected void LogFinish (Player player) {
            Debug.Log ($"{player.name}:Finished using an ability. ({Time.time})");
        }

        
    }

}