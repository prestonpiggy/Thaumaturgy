using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TurkeyWork.Stats;

namespace TurkeyWork.Items {
    [CreateAssetMenu(menuName ="TurkeyWork/Item")]
    public class Item : Blueprint<ItemComponent> {

        public Sprite sprite;
        public StatAttribute[] statAttributes;

        [ReadOnly, ShowInInspector] int instanceID;

        [System.Serializable]
        public struct StatAttribute{
            public string TargetStat;
            public int Amount;
            [System.NonSerialized] public int instanceID;

            public Modifier GetAsModifier()
            {
                Modifier modifier = new Modifier(instanceID) {
                    FlatAmount = Amount
                };
                return modifier;
            }
        }

        private void OnEnable()
        {
            instanceID = GetInstanceID();
            for (int i = 0; i < statAttributes.Length; i++)
            {
                statAttributes[i].instanceID = instanceID;
            }
        }
    }

}