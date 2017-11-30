using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Stat Type")]
    public class StatType : ScriptableObject, IEqualityComparer<StatType> {

        static Dictionary<string, StatType> statTypeDictionary = new Dictionary<string, StatType> ();

        [ReadOnly, ShowInInspector] int id;

        [SerializeField, ReadOnly] internal bool isResourceComponent;

        [TextArea (4, 10)]
        [SerializeField] string description;

        public string Description => description;
        public bool IsResourceComponent => isResourceComponent;
        public int ID => id;

        StatType () {
            isResourceComponent = true;
        }

        public static StatType FromName (string name) {
            return statTypeDictionary[name];
        }

        public bool Equals (StatType x, StatType y) {
            return x.id == y.id;
        }

        public int GetHashCode (StatType obj) {
            return id;
        }

        public override int GetHashCode () {
            return id;
        }

        public override bool Equals (object other) {
            if (other == null)
                return false;
            return (other as StatType)?.id == id;
        }

        private void OnEnable () {
            id = GetInstanceID ();
            statTypeDictionary.Add (name, this);
        }
    }

}