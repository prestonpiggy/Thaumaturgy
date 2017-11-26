using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Players {

    [System.Serializable]
    public sealed class PlayerProfile {

        [SerializeField] private string name = "<PlayerName>";
        public string Name => name;
    }

}