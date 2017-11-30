using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Events;
using TurkeyWork.Stats;

namespace TurkeyWork.Actors {

    [HideMonoScript]
    public class ActorAttributes : ActorComponent, Management.ISaveHandler {

        [AssetsOnly]
        public AttributeData DefaultAttributeData;

        static List<BuffStatLink> timedBuffs = new List<BuffStatLink> ();
        static bool buffUpdateDone;

        Dictionary<StatType, Stat> statsDictionary = new Dictionary<StatType, Stat> ();

        Resource[] resources;
        Stat[] stats;

        #region Fetches
        public Stat this[StatType type] {
            get {
                return statsDictionary[type];
            }
        }

        /// <summary>
        /// Not recomended! This is error prone and slower than the StatType alternative.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public Stat this[string typeName] {
            get {
                return statsDictionary[StatType.FromName (typeName)];
            }
        }

        public Resource this[ResourceType type] {
            get {
                return resources.First ((res) => res.Type.Equals (type));
            }
        }

        [System.Obsolete ("Consider using a StatType instead")]
        public bool TryGetStat (string typeName, out Stat stat) {
            return statsDictionary.TryGetValue (StatType.FromName (typeName), out stat);
        }

        public bool TryGetStat (StatType type, out Stat stat) {
            return statsDictionary.TryGetValue (type, out stat);
        }
        #endregion Fetches

        public void RegisterTimedBuff (StatBase stat, Modifier buff) {
            // Check if the buff is actually permanent and, thus, should not be added.
            if (buff.IsPermanent)
                return;
            timedBuffs.Add (new BuffStatLink (stat, buff));
        }

        protected override void Awake () {
            base.Awake ();
            //RegisterStats ();
        }

        private void Update () {

        }

        private void LateUpdate () {
            buffUpdateDone = false;
        }

        private void OnValidate () {
            /*
            Health.Percent = Health.Current.Value / (float) Health.MaxValue.Value;
            Mana.Percent = Health.Current.Value / (float) Mana.MaxValue.Value;
            Stamina.Percent = Health.Current.Value / (float) Stamina.MaxValue.Value;
            Armor.Percent = Health.Current.Value / (float) Armor.MaxValue.Value;
            Aegis.Percent = Health.Current.Value / (float) Aegis.MaxValue.Value;
            */
        }

        public void OnLoadData () {
            AttributeData saveData;

            if (Management.SaveSystem.Load ("Attributes", out saveData)) {
                foreach (var stat in saveData.Resources) {
                   
                }
            }
        }

        public void OnSaveData () {

        }

        struct BuffStatLink {
            public readonly StatBase Stat;
            public readonly Modifier Buff;

            public BuffStatLink (StatBase stat, Modifier buff) {
                Stat = stat;
                Buff = buff;
            }

            public void RemoveBuff () {
                Stat.RemoveBuffAndRecalculate (Buff);
            }
        }
    }

}