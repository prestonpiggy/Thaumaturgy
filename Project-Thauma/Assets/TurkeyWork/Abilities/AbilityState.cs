using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    internal abstract class AbilityState {

        [System.Flags]
        internal enum AbilityPropertyFlag { Animated = 1, Cancelable = 2, Interruptable = 4}

        [SerializeField, EnumToggleButtons, Title ("Common"), HideLabel]
        AbilityPropertyFlag propertyFlags;

        [ShowIf ("IsAnimated")]
        [SerializeField] string animation;

        public bool IsInterruptable => propertyFlags.HasFlag (AbilityPropertyFlag.Interruptable);
        public bool IsAnimated => propertyFlags.HasFlag (AbilityPropertyFlag.Animated);

        public abstract void OnState (IAbilityUser user, ref AbilityInfo info);

        public virtual void OnCancel (IAbilityUser user, ref AbilityInfo info) {

        }

        public virtual void OnInterrupt (IAbilityUser user, ref AbilityInfo info) {

        }

    }

}