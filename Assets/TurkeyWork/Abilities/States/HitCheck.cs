using System.Collections;
using System.Collections.Generic;
using TurkeyWork.Actors;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Abilities {

    [System.Serializable]
    public class HitCheck : AbilityState {

        public LayerMask HitMask;

        public enum HitAreaType { Arc, Box, Circle }
        [SerializeField] HitAreaType hitShape;

        [HideIf ("hitShape", HitAreaType.Box)]
        public float AttackRange;
        [ShowIf ("hitShape", HitAreaType.Arc)]
        public Arc HitArc = new Arc (-60f, 30);
        [ShowIf ("hitShape", HitAreaType.Box)]
        public Vector2 BoxShape = new Vector2 (2f, 0.5f);

        public override void ResolveState (ActorBody actor, ref AbilityInfo abilityInfo) {
            
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