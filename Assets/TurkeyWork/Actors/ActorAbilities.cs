using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Stats;
using TurkeyWork.Abilities;

namespace TurkeyWork.Actors {

    public class ActorAbilities : ActorComponent, IAbilityUser {

        ActorAttributes attributes;

        public AbilitySlot[] AbilitySlots;

        public List<Passive> PassiveAbilities;

        public MonoBehaviour Behaviour => this;

        public bool DeductStat (StatCost statCost) {
            Stat stat;

            if (!attributes.TryGetStat (statCost.StatType, out stat))
                return false;

            return true;
        }

        public void OnAbilityCanceled () {
            throw new System.NotImplementedException ();
        }

        public void OnAbilityInterrupted () {
            throw new System.NotImplementedException ();
        }

        private void OnControllerUpdate () {
            // Handler Ability Usage
        }

        protected override void Awake () {
            base.Awake ();
            attributes = GetComponent<ActorAttributes> ();
        }

        [System.Serializable]
        public struct AbilitySlot {
            public Skill Ability;
            public AbilityInfo Status;
        }

    }

}