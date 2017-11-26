using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Stats {

    [System.Serializable]
    public struct Modifier : IEqualityComparer<Modifier> {
        public readonly int ID;

        public float ExpireTime;
        public float Multiplier;
        public int FlatAmount;

        public bool IsPermanent => ExpireTime == -1;

        public Modifier (int id) {
            ID = id;
            Multiplier = 1;
            FlatAmount = 0;
            ExpireTime = -1;
        }

        public bool Equals (Modifier x, Modifier y) {
            return x.ID == y.ID;
        }

        public int GetHashCode (Modifier obj) {
            return ID.GetHashCode ();
        }
    }

}
