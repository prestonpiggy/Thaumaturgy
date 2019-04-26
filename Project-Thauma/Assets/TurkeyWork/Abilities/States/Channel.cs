using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    internal class Channel : AbilityState {

        [Title ("Specific"), Tooltip ("This is mesured in Fixed Time Frames")]
        [HorizontalGroup ("Duration")]
        public int Duration;

        [HorizontalGroup ("Duration", MaxWidth = 80), Title(""), HideLabel]
        [ShowInInspector, ReadOnly] public float DurationInSeconds => Duration * Time.fixedDeltaTime;

        public override void OnState (IAbilityUser user, ref AbilityInfo info) {

        }

    }

}