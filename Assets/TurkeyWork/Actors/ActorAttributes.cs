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

        [FoldoutGroup ("Defenses"), Title ("Armor"), HideLabel]
        public Resource Armor;
        [FoldoutGroup ("Defenses"), Title ("Aegis"), HideLabel]
        public Resource Aegis; // Like magic resistance

        [FoldoutGroup ("Movement"), Title ("Speed"), HideLabel]
        public Stat MovementSpeed = new Stat (300);
        [FoldoutGroup ("Movement"), Title ("Jump Height"), HideLabel]
        public Stat JumpHeight = new Stat (240);
        [FoldoutGroup ("Movement"), Title ("Gravity Scale"), HideLabel]
        public Stat GravityScale = new Stat (200); 

        public bool TryGetStat (string name, out Stat stat) {
            return statsRegistry.TryGetValue (name, out stat);
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
        }

        void RegisterStats () {
            statsRegistry = new Dictionary<string, Stat> () {
                { "Health Current", Health.Current },
                { "Health Max", Health.MaxValue },
                { "Health Regen", Health.Regen },
                { "Health Regen Start Delay", Health.RegenStartDelay },

                { "Mana Current", Mana.Current },
                { "Mana Max", Mana.MaxValue },
                { "Mana Regen", Mana.Regen },
                { "Mana Regen Start Delay", Mana.RegenStartDelay },

                { "Stamina Current", Stamina.Current },
                { "Stamina Max", Stamina.MaxValue },
                { "Stamina Regen", Stamina.Regen },
                { "Stamina Regen Start Delay", Stamina.RegenStartDelay },

                { "Armor Current", Armor.Current },
                { "Armor Max", Armor.MaxValue },
                { "Armor Regen", Armor.Regen },
                { "Armor Regen Start Delay", Armor.RegenStartDelay },

                { "Aegis Current", Aegis.Current },
                { "Aegis Max", Aegis.MaxValue },
                { "Aegis Regen", Aegis.Regen },
                { "Aegis Regen Start Delay", Aegis.RegenStartDelay },

                { "Movement Speed", MovementSpeed },
                { "Jump Height", JumpHeight },
                { "Gravity Scale", GravityScale }
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