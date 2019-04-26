using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [System.Serializable]
    public class Stat {

        public StatType Type;

        [HorizontalGroup, OnValueChanged ("Recalculate")]
        public int Base = 4;
        [ReadOnly, HorizontalGroup] public int Value = 4;

        public float Value001 => Value / 100f;

        [ReadOnly]
        public List<Modifier> Modifiers = new List<Modifier> ();

        public Stat (StatType type) {
            Type = type;
        }

        public Stat (StatType type, int baseValue) {
            Type = type;
            Base = baseValue;
            Value = baseValue;
        }

        public void AddBuff (Modifier mod) {
            Modifiers.Add (mod);
        }

        public void RemoveBuff (Modifier mod) {
            Modifiers.Remove (mod);
        }

        public void ClearAllBuffs () {
            Modifiers.Clear ();
            Recalculate ();
        }

        public void AddBuffAndRecalculate (Modifier mod) {
            Modifiers.Add (mod);
            Recalculate ();
        }

        public void RemoveBuffAndRecalculate (Modifier mod) {
            Modifiers.Remove (mod);
            Recalculate ();
        }

        public void Recalculate () {
            Value = Base;
            ApplyFlat ();
            ApplyMult ();
        }

        void ApplyFlat () {
            foreach (var mod in Modifiers) {
                Value += mod.FlatAmount;
            }
        }

        void ApplyMult () {
            if (Modifiers.Count > 0) {
                float mult = 0;

                foreach (var mod in Modifiers) {
                    mult += mod.Multiplier;
                }
                Value = (int) (Value * Mathf.Clamp (mult, 0, float.MaxValue));
            }
        }

    }

}