using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

using TurkeyWork.Actors;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "TurkeyWork/Abilities/Melee Attack")]
    public class MeleeAttack : Ability {

        public enum HitAreaType { Arc, Box, Circle }
        [SerializeField] HitAreaType hitShape;

        public LayerMask HitMask;

        public float DamageDelay;

        [HideIf ("hitShape", HitAreaType.Box)]
        public float AttackRange;
        [ShowIf ("hitShape", HitAreaType.Arc)]
        public Arc HitArc = new Arc (-60f, 30);
        [ShowIf ("hitShape", HitAreaType.Box)]
        public Vector2 BoxShape = new Vector2 (2f, 0.5f);

        public string AnimationName;

        public override IEnumerator<AbilityInfo> Use (Player player) {
            LogStart (player);

            var attributes = player.Attributes;
            var buff = new Buff (new System.Guid ()) {
                Multiplier = MovementSpeedMultiplier,
                ExpireTime = Time.time + SpeedMultiplierDuration
            };

            attributes.RegisterTimedBuff (attributes.MovementSpeed, buff);
            attributes.MovementSpeed.AddBuffAndRecalculate (buff);

            var wait = new AbilityInfo () {
                WaitUntil = Time.time + DamageDelay
            };
            yield return wait;

            DoHitDetection (player);

            wait.IsDone = true;

            LogFinish (player);
            yield return wait;
        }    
        
        public List<ActorBody> DoHitDetection (Player player) {
            var hitMask = HitMask ^ player.gameObject.layer;
            Collider2D[] hitColliders;
            switch (hitShape) {
            case HitAreaType.Arc:
                hitColliders = Physics2D.OverlapCircleAll (player.transform.position, AttackRange, hitMask);
                return DamageHitTargets (hitColliders);
            case HitAreaType.Box:
                hitColliders = Physics2D.OverlapBoxAll (player.transform.position, BoxShape, 0f);
                break;
            case HitAreaType.Circle:
                hitColliders = Physics2D.OverlapCircleAll (player.transform.position, AttackRange, hitMask);
                return DamageHitTargets (hitColliders);
            }
            return null;
        }
       

        [System.Serializable]
        public struct Arc {
            public float From;
            public float To;

            public Arc (float from, float to) {
                From = from;
                To = to;
            }
        }
    }

}