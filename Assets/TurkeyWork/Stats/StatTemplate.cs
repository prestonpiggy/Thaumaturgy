using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Stat Template")]
    public class StatTemplate : ScriptableObject {

        [ListDrawerSettings (NumberOfItemsPerPage = 1)]
        [SerializeField] List<Resource> resources;

        [ListDrawerSettings (NumberOfItemsPerPage = 8)]
        [SerializeField] List<Stat> simpleStats;

        public Resource[] Resources => resources.ToArray ();
        public Stat[] Stats => simpleStats.ToArray ();

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
        }
    }

}
