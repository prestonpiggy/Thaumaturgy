using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public struct Buff : IEqualityComparer<Buff> {
        public readonly System.Guid ID;
        public float ExpireTime;
        public float Multiplier;
        public int FlatAmount;

        public bool IsPermanent => ExpireTime == -1;

        public Buff (System.Guid id) {
            ID = id;
            Multiplier = 1;
            FlatAmount = 0;
            ExpireTime = -1;
        }

        public bool Equals (Buff x, Buff y) {
            return x.ID == y.ID;
        }

        public int GetHashCode (Buff obj) {
            return ID.GetHashCode ();
        }
    }

}
