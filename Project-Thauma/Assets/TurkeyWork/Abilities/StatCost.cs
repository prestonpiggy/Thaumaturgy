using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Stats;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public struct StatCost {
        [AssetsOnly]
        public StatType StatType;
        public int Amount;

        public string TargetStatName => StatType.name;
    }

}