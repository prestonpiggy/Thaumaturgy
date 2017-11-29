using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Resource Type")]
    [HideMonoScript]
    public class ResourceType : ScriptableObject, IEqualityComparer<ResourceType> {

        [ShowInInspector, ReadOnly] int id;

        [TextArea (4, 10)]
        [SerializeField] string description;

        [BoxGroup ("Linked Stats")]
        [SerializeField, AssetsOnly] StatType current;
        [BoxGroup ("Linked Stats")]
        [SerializeField, AssetsOnly] StatType maximum;
        [BoxGroup ("Linked Stats")]
        [SerializeField, AssetsOnly] StatType regeneration;
        [BoxGroup ("Linked Stats")]
        [SerializeField, AssetsOnly] StatType regenerationDelay;

        public string Description => description;
        public int ID => id;

        public StatType Curret => current;
        public StatType Maximum => maximum;
        public StatType Regeneration => regeneration;
        public StatType RegenerationDelay => regenerationDelay;

        private void OnEnable () {
            id = GetInstanceID ();   
        }

        [Button ("Find Stats")]
        void AutoFindStats () {
            var allStats = Resources.LoadAll<StatType> ("");

            var relevantStats = allStats.Where (s => s.name.Contains (name));

            foreach (var stat in relevantStats) {
                if (stat.name.Contains ("Current"))
                    current = stat;
                else if (stat.name.Contains ("Max") || stat.name.Contains ("Maximum"))
                    maximum = stat;
                else if (stat.name.Contains ("Delay"))
                    regenerationDelay = stat;
                else if (stat.name.Contains ("Regeneration"))
                    regeneration = stat;
            }

#if UNITY_EDITOR
            UnityEditor.AssetDatabase.SaveAssets ();
#endif
        }

        public bool Equals (ResourceType x, ResourceType y) {
            return x.id == y.id;
        }

        public int GetHashCode (ResourceType obj) {
            return id;
        }
    }

}
