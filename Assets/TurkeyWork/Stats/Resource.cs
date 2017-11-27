using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [System.Serializable]
    public class Resource {
        [Title ("Current"), HideLabel, OnValueChanged ("Recalculate")] public Stat Current = new Stat (100);
        [ProgressBar (0, 1, ColorMember = "EDITOR_BarColor"), ReadOnly, HideLabel]
        [CustomContextMenu ("Blue", "ColorBlue"), CustomContextMenu ("Green", "ColorGreen"), 
            CustomContextMenu ("Red", "ColorRed"), CustomContextMenu ("Yellow", "ColorYellow"),
            CustomContextMenu ("White", "ColorWhite"), CustomContextMenu ("Cyan", "ColorCyan")]
        public float Percent;

        [Title ("Max"), HideLabel, OnValueChanged ("Recalculate")]
        public Stat MaxValue = new Stat (100);

        [Title ("Regeneration"), HideLabel]
        public Stat Regen;
        [Title ("Regen Delay"), HideLabel]
        public Stat RegenStartDelay;

        [SerializeField, ReadOnly] private float lastExpenditure;

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