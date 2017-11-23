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

        [AssetsOnly]
        public GameEvent OnDeathEvent;

        [FoldoutGroup ("HEALTH", expanded: false), HideLabel]
        public Resource Health;
        [FoldoutGroup ("STAMINA", expanded: false), HideLabel]
        public Resource Stamina;
        [FoldoutGroup ("MANA", expanded: false), HideLabel]
        public Resource Mana;

        [FoldoutGroup ("Movement"), Title ("Speed"), HideLabel]
        public Stat MovementSpeed = new Stat (300);
        [FoldoutGroup ("Movement"), Title ("Jump Height"), HideLabel]
        public Stat JumpHeight = new Stat (240);
        [FoldoutGroup ("Movement"), Title ("Gravity Scale"), HideLabel]
        public Stat GravityScale = new Stat (200);

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

            if (Health.Current <= 0) {
                Health.Current = 0;
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
            Health.Percent = Health.Current / (float) Health.MaxValue.Value;
        }

        public void RegisterTimedBuff (Stat stat, Modifier buff) {
            if (buff.IsPermanent)
                return;
            timedBuffs.Add (new BuffStatLink (stat, buff));
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