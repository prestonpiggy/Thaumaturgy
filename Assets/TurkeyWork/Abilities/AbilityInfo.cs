using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TurkeyWork.Actors;

namespace TurkeyWork.Abilities {

    public class AbilityInfo : CustomYieldInstruction {
        public bool IsDone;
        public float WaitUntil;

        public AbilityState CurrentState;
        public ActorBody[] hitActors;

        public AbilityInfo () {
        }

        public AbilityInfo (float waitDuration) {
            WaitUntil = waitDuration + Time.time;
        }

        public override bool keepWaiting {
            get {
                return Time.time < WaitUntil;
            }
        }
    }

}