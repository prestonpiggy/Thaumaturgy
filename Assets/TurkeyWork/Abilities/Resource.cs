﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public class Resource {
        [Title ("Current"), HideLabel, OnValueChanged ("Recalculate")] public int Current = 100;
        [ProgressBar (0, 1, ColorMember = "EDITOR_BarColor"), ReadOnly, HideLabel]
        [CustomContextMenu ("Blue", "ColorBlue"), CustomContextMenu ("Green", "ColorGreen"), CustomContextMenu ("Red", "ColorRed"), CustomContextMenu ("Yellow", "ColorYellow")]
        public float Percent;

        //[Title ("Base"), HideLabel]
        //public Stat BaseValue = new Stat (100);
        [Title ("Max"), HideLabel, OnValueChanged ("Recalculate")]
        public Stat MaxValue = new Stat (100);

        [Title ("Regeneration"), HideLabel]
        public Stat Regen;
        [Title ("Regen Delay"), HideLabel]
        public Stat RegenStartDelay;

        [SerializeField, ReadOnly] private float lastExpenditure;

        public void Regenerate (float deltaTime) {
            if (Time.time < lastExpenditure + RegenStartDelay.Value)
                return;

            Current = Mathf.Clamp (Current + (int) (Regen.Value * deltaTime), 0, MaxValue.Value);
            Percent = Current / (float) MaxValue.Value;
        }

        public void Recalculate () {
            MaxValue.Recalculate ();
            Current = Mathf.Clamp (Current, 0, MaxValue.Value);
            Percent = Current / (float) MaxValue.Value;
        }

#if UNITY_EDITOR

        void ColorGreen () {
            EDITOR_BarColor = Color.green;
        }

        void ColorRed () {
            EDITOR_BarColor = Color.red;
        }

        void ColorBlue () {
            EDITOR_BarColor = Color.blue;
        }

        void ColorYellow () {
            EDITOR_BarColor = Color.yellow;
        }
        Color EDITOR_BarColor = Color.blue;
#endif
    }

}