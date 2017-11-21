using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public class Stat {
        [HorizontalGroup, OnValueChanged ("Recalculate")]
        public int Base;
        [ReadOnly, HorizontalGroup] public int Value;

        public float Value001 => Value / 100f;

        public List<Buff> buffs;

        public Stat (int baseValue) {
            Base = baseValue;
            Value = baseValue;
            buffs = new List<Buff> ();
        }

        public void AddBuff (Buff buff) {
            buffs.Add (buff);
        }

        public void RemoveBuff (Buff buff) {
            buffs.Remove (buff);
        }

        public void AddBuffAndRecalculate (Buff buff) {
            buffs.Add (buff);
            Recalculate ();
        }

        public void RemoveBuffAndRecalculate (Buff buff) {
            buffs.Remove (buff);
            Recalculate ();
        }

        public void Recalculate () {
            Value = Base;
            ApplyFlat ();
            ApplyMult ();
        }

        void ApplyFlat () {
            foreach (var buff in buffs) {
                Value += buff.FlatAmount;
            }
        }

        void ApplyMult () {           
            if (buffs.Count > 0) {
                float mult = 0;

                foreach (var buff in buffs) {
                    mult += buff.Multiplier;
                }
                Value = (int) (Value * Mathf.Clamp (mult, 0, float.MaxValue));
            }         
        }
    }

}