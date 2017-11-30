using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Attribute Template")]
    public class AttributeData : ScriptableObject {

        Dictionary<StatType, Stat> statDictionary;
        Dictionary<ResourceType, Resource> recourceDictionary;

        [SerializeField] AttributeData parent;

        [ListDrawerSettings (NumberOfItemsPerPage = 1)]
        [SerializeField] List<Resource> resources;

        [ListDrawerSettings (NumberOfItemsPerPage = 8)]
        [SerializeField] List<Stat> simpleStats;

        public IEnumerable<Resource> Resources => resources;
        public IEnumerable<Stat> Stats => simpleStats;

        #region Getters
        public Stat this[StatType type] {
            get {
                return statDictionary[type];
            }
        }

        public Stat this[string typeName] {
            get {
                return statDictionary[StatType.FromName (typeName)];
            }
        }

        public Resource this[ResourceType type] {
            get {
                return resources.First ((res) => res.Type.Equals (type));
            }
        }

        [System.Obsolete ("Consider using a StatType instead")]
        public bool TryGetStat (string typeName, out Stat stat) {
            return statDictionary.TryGetValue (StatType.FromName (typeName), out stat);
        }

        public bool TryGetStat (StatType type, out Stat stat) {
            return statDictionary.TryGetValue (type, out stat);
        }
        #endregion Getters

        public void UpdateStats (float deltaTime) {
            foreach (var res in resources) {
                res.Update (deltaTime);
                Debug.Log (res.Type.name);
            }
        }

        private void OnEnable () {
            statDictionary = new Dictionary<StatType, Stat> ();
            recourceDictionary = new Dictionary<ResourceType, Resource> ();
        }

#if UNITY_EDITOR
        [Button]
        void AddAllFromProject () {
            var resourceTypes = UnityEngine.Resources.LoadAll<ResourceType> ("");
            var statTypes = UnityEngine.Resources.LoadAll<StatType> ("");

            foreach (var resType in resourceTypes) {
                if (Resources.Any (res => res.Type.Equals (resType)))
                    continue;
                resources.Add (new Resource (resType));
            }

            foreach (var statType in statTypes) {
                if (statType.IsResourceComponent || simpleStats.Any (res => res.Type.Equals (statType)))
                    continue;
                simpleStats.Add (new Stat (statType));
            }

            resources.Sort ((x, y) => x.Type.name.CompareTo (y.Type.name));
            simpleStats.Sort ((x, y) => x.Type.name.CompareTo (y.Type.name));
            UnityEditor.AssetDatabase.SaveAssets ();
        }
#endif
    }

}
