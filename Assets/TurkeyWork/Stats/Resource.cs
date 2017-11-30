using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TurkeyWork.Stats {

    [System.Serializable]
    public class Resource : IEqualityComparer<Resource> {

        public ResourceType Type;
        public float Percent;

        [Title ("Current"), HideLabel, OnValueChanged ("Recalculate")]
        public Stat Current;
       
        [Title ("Max"), HideLabel, OnValueChanged ("Recalculate")]
        public Stat MaxValue;

        [Title ("Regeneration"), HideLabel]
        public Stat Regen;

        [Title ("Regen Delay"), HideLabel]
        public Stat RegenStartDelay;

        [SerializeField, ReadOnly] private float lastExpenditure;

        public Resource (ResourceType type) {
            Type = type;
            Current = new Stat (type.Curret);
            MaxValue = new Stat (type.Maximum);
            Regen = new Stat (type.Regeneration);
            RegenStartDelay = new Stat (type.RegenerationDelay);
        }

        public void SetFull () {
            Current.Value = MaxValue.Value;
        }

        public void Regenerate (float deltaTime) {
            if (Time.time < lastExpenditure + RegenStartDelay.Value)
                return;

            Current.Value = Mathf.Clamp (Current.Value + (int) (Regen.Value * deltaTime), 0, MaxValue.Value);
            Percent = Current.Value / (float) MaxValue.Value;
        }

        public void Recalculate () {
            MaxValue.Recalculate ();
            Current.Value = Mathf.Clamp (Current.Value, 0, MaxValue.Value);
            Percent = Current.Value / (float) MaxValue.Value;
        }

        public bool Equals (Resource x, Resource y) {
            return x.Type.Equals (y.Type);
        }

        public int GetHashCode (Resource obj) {
            return obj.Type.ID;
        }

#if UNITY_EDITOR
        void ColorGreen () { EDITOR_BarColor = Color.green; }
        void ColorRed () { EDITOR_BarColor = Color.red; }
        void ColorBlue () { EDITOR_BarColor = Color.blue; }
        void ColorYellow () { EDITOR_BarColor = Color.yellow; }
        void ColorCyan () { EDITOR_BarColor = Color.cyan; }
        void ColorWhite () { EDITOR_BarColor = Color.white; }

        [HideInInspector, SerializeField] Color EDITOR_BarColor = Color.blue;
#endif
    }

}