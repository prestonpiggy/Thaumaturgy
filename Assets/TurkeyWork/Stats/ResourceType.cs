using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Resource Type")]
    [HideMonoScript]
    public class ResourceType : ScriptableObject, IEqualityComparer<ResourceType> {

        static Dictionary<string, ResourceType> resourceTypeDictionary = new Dictionary<string, ResourceType> ();

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

        public static ResourceType FromName (string typeName) {
            return resourceTypeDictionary[typeName];
        }

        private void OnEnable () {
            id = GetInstanceID ();
            resourceTypeDictionary.Add (name, this);
        }

#if UNITY_EDITOR

        [ButtonGroup()]
        void CreateMissingStats () {
            var so = new SerializedObject (this);
            AddMissing (current, "Current", so);
            AddMissing (maximum, "Max", so);
            AddMissing (regeneration, "Regen", so);
            AddMissing (regenerationDelay, "Regen Delay", so);
            AssetDatabase.SaveAssets ();
            LinkStats ();
        }
        
        void AddMissing (StatType type, string typeName, SerializedObject parent) {
            if (type == null) {
                var st = CreateInstance<StatType> ();
                st.name = $"{name} {typeName}";
                st.isResourceComponent = true;
                AssetDatabase.AddObjectToAsset (st, parent.targetObject);
            }
        }

        [ButtonGroup ()]
        void LinkStats () {
            var allStats = Resources.LoadAll<StatType> ("");

            var relevantStats = allStats.Where (s => s.name.Contains (name));

            foreach (var stat in relevantStats) {
                if (stat.name.Contains ("Current"))
                    current = stat;
                else if (stat.name.Contains ("Max") || stat.name.Contains ("Maximum"))
                    maximum = stat;
                else if (stat.name.Contains ("Delay"))
                    regenerationDelay = stat;
                else if (stat.name.Contains ("Regen"))
                    regeneration = stat;
            }

            AssetDatabase.SaveAssets ();
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
