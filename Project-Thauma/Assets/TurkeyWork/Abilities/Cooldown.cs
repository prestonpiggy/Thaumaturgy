using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public struct Cooldown {

        public int Duration;
        public int StartFrame;

        public bool IsActive => StartFrame + Duration < BoltNetwork.frame; 
    }

}
