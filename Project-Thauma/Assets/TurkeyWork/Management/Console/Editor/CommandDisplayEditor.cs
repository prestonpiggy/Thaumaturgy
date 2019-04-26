using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace TurkeyWork.Management {

    [CustomEditor (typeof (CommandRegistryDisplay))]
    public class CommandDisplayEditor : Editor {

        new CommandRegistryDisplay target;

        private void OnEnable () {
            target = base.target as CommandRegistryDisplay;
        }

        public override void OnInspectorGUI () {
            base.OnInspectorGUI ();

            EditorGUILayout.LabelField ("Commands", EditorStyles.largeLabel);

            if (target.Commands == null)
                return;

            foreach (var com in target.Commands) {
                EditorGUILayout.LabelField (com.Name);
            }
        }
    }

}