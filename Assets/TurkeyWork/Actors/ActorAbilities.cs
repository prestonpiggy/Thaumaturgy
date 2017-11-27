﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Abilities;

namespace TurkeyWork.Actors {

    public class ActorAbilities : ActorComponent {

        public AbilitySlot[] AbilitySlots;

        public List<Passive> PassiveAbilities;

        private void OnControllerUpdate () {
            /*
            if (abilityRoutine != null) {

                if (abilityOverride = abilityRoutine.Current.IsDone) {
                    abilityRoutine = null;
                }
            } else if (Input.GetKeyDown (KeyCode.Mouse0)) {
                if (abilityRoutine == null) {
                    AbilityInfo = TestAbility.Use (this);
                    StartCoroutine (abilityRoutine);
                }
            }
            */
        }

        [System.Serializable]
        public struct AbilitySlot {
            public Skill Ability;
            public AbilityInfo Status;
        }

    }

}