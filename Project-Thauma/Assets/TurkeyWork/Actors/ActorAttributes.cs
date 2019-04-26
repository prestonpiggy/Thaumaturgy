using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Events;
using TurkeyWork.Stats;
using TurkeyWork.Management;

namespace TurkeyWork.Actors {

    [HideMonoScript]
    public class ActorAttributes : ActorComponent, Management.ISaveHandler {

        [AssetsOnly]
        [SerializeField] AttributeData DefaultAttributeData;
        [ShowInInspector, ReadOnly, InlineEditor] AttributeData runtimeData;

        public AttributeData RuntimeData => runtimeData;

        public string SaveFileName => "Attributes.json";

        #region Gettters
        public Stat this[StatType type] {
            get {
                return runtimeData[type];
            }
        }

        public Stat this[string typeName] {
            get {
                return runtimeData[typeName];
            }
        }

        public Resource this[ResourceType type] {
            get {
                return runtimeData[type];
            }
        }

        [System.Obsolete ("Consider using a StatType instead")]
        public bool TryGetStat (string typeName, out Stat stat) {
            return runtimeData.TryGetStat (typeName, out stat);
        }

        public bool TryGetStat (StatType type, out Stat stat) {
            return runtimeData.TryGetStat (type, out stat);
        }
        #endregion Getters

        #region Save/Load
        public void OnLoadData () {
            if (!SaveManager.Load (SaveFileName, runtimeData)) {
                runtimeData = Instantiate (DefaultAttributeData);
            }
            foreach (var res in runtimeData.Resources)
                res.Recalculate ();
        }

        public void OnSaveData () {
            SaveManager.Save (SaveFileName, runtimeData ?? DefaultAttributeData);
        }
        #endregion Save/laod

        protected override void Awake () {
            base.Awake ();
            runtimeData = Instantiate (DefaultAttributeData);
        }

        // This shuold happen in Refresh Update
        private void Update () {
            runtimeData.UpdateStats (Time.deltaTime);
        }

    }

}