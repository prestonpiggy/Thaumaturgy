using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Events;
using TurkeyWork.Stats;

namespace TurkeyWork.Actors {

    [HideMonoScript]
    public class ActorAttributes : ActorComponent {

        static List<BuffStatLink> timedBuffs = new List<BuffStatLink> ();
        static bool buffUpdateDone;

        // This will probably get replaced with a static string to int table and a list for all actors containen indexed references 
        Dictionary<string, Stat> statsRegistry;

        [AssetsOnly]
        public GameEvent OnDeathEvent;

        [FoldoutGroup ("HEALTH", expanded: false), HideLabel]
        public Resource Health;
        [FoldoutGroup ("STAMINA", expanded: false), HideLabel]
        public Resource Stamina;
        [FoldoutGroup ("MANA", expanded: false), HideLabel]
        public Resource Mana;

        [FoldoutGroup ("ARMOR", expanded: false), HideLabel]
        public Resource Armor;
        [FoldoutGroup ("AEGIS", expanded: false), HideLabel]
        public Resource Aegis; // Like magic resistance

        [FoldoutGroup ("MOVEMENT", expanded: false), Title ("Speed"), HideLabel]
        public Stat MovementSpeed = new Stat (300);
        [FoldoutGroup ("MOVEMENT", expanded: false), Title ("Jump Height"), HideLabel]
        public Stat JumpHeight = new Stat (240);
        [FoldoutGroup ("MOVEMENT", expanded: false), Title ("Gravity Scale"), HideLabel]
        public Stat GravityScale = new Stat (200);

        [FoldoutGroup ("DAMAGE", expanded: false), Title ("Physical Damage"), HideLabel]
        public Stat DamagePhysical = new Stat (10);
        [FoldoutGroup ("DAMAGE", expanded: false), Title ("Magical Damage"), HideLabel]
        public Stat DamageMagical = new Stat (10);

        [System.Obsolete ("Consider using a StatType instead")]
        public bool TryGetStat (string name, out Stat stat) {
            return statsRegistry.TryGetValue (name, out stat);
        }

        public bool TryGetStat (StatType type, out Stat stat) {
            return statsRegistry.TryGetValue (type.name, out stat);
        }

        public void RegisterTimedBuff (Stat stat, Modifier buff) {
            // Check if the buff is actually permanent and, thus, should not be added.
            if (buff.IsPermanent)
                return;
            timedBuffs.Add (new BuffStatLink (stat, buff));
        }

        protected override void Awake () {
            base.Awake ();
            RegisterStats ();
        }

        private void Update () {
            if (!buffUpdateDone) {
                for (int i = 0; i < timedBuffs.Count; i++) {
                    if (timedBuffs[i].Buff.ExpireTime <= Time.time) {
                        timedBuffs[i].RemoveBuff ();
                        timedBuffs.RemoveAt (i);
                        --i;
                    }
                }
                buffUpdateDone = true;
            }

            if (Health.Current.Value <= 0) {
                Health.Current.Value = 0;
                OnDeathEvent.Raise ();
                gameObject.SetActive (false);
                return;
            }

            Health.Regenerate (Time.deltaTime);
            Stamina.Regenerate (Time.deltaTime);
            Mana.Regenerate (Time.deltaTime);
        }

        private void LateUpdate () {
            buffUpdateDone = false;
        }

        private void OnValidate () {
            Health.Percent = Health.Current.Value / (float) Health.MaxValue.Value;
            Mana.Percent = Health.Current.Value / (float) Mana.MaxValue.Value;
            Stamina.Percent = Health.Current.Value / (float) Stamina.MaxValue.Value;
            Armor.Percent = Health.Current.Value / (float) Armor.MaxValue.Value;
            Aegis.Percent = Health.Current.Value / (float) Aegis.MaxValue.Value;
        }

        void RegisterStats () {
            statsRegistry = new Dictionary<string, Stat> () {
                { "Health Current", Health.Current },
                { "Health Max", Health.MaxValue },
                { "Health Regen", Health.Regen },
                { "Health Regen Delay", Health.RegenStartDelay },

                { "Mana Current", Mana.Current },
                { "Mana Max", Mana.MaxValue },
                { "Mana Regen", Mana.Regen },
                { "Mana Regen Delay", Mana.RegenStartDelay },

                { "Stamina Current", Stamina.Current },
                { "Stamina Max", Stamina.MaxValue },
                { "Stamina Regen", Stamina.Regen },
                { "Stamina Regen Delay", Stamina.RegenStartDelay },

                { "Armor Current", Armor.Current },
                { "Armor Max", Armor.MaxValue },
                { "Armor Regen", Armor.Regen },
                { "Armor Regen Delay", Armor.RegenStartDelay },

                { "Aegis Current", Aegis.Current },
                { "Aegis Max", Aegis.MaxValue },
                { "Aegis Regen", Aegis.Regen },
                { "Aegis Regen Delay", Aegis.RegenStartDelay },

                { "Movement Speed", MovementSpeed },
                { "Jump Height", JumpHeight },
                { "Gravity Scale", GravityScale },

                { "Damage Physical", DamagePhysical },
                { "Damage Macigal", DamageMagical },
            };
        }

        struct BuffStatLink {
            public readonly Stat Stat;
            public readonly Modifier Buff;

            public BuffStatLink (Stat stat, Modifier buff) {
                Stat = stat;
                Buff = buff;
            }

            public void RemoveBuff () {
                Stat.RemoveBuffAndRecalculate (Buff);
            }
        }
    }

}