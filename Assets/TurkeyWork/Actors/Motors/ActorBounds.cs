using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Serializable]
    public struct ActorBounds {
        [SerializeField] Vector2 offset;
        [SerializeField] Vector2 expands;

        Vector2 Origin;

        public Vector2 TopLeft => new Vector2 (Origin.x - expands.x, Origin.y + expands.y);
        public Vector2 TopRight => new Vector2 (Origin.x + expands.x, Origin.y + expands.y);
        public Vector2 BottomLeft => new Vector2 (Origin.x - expands.x, Origin.y - expands.y);
        public Vector2 BottomRight => new Vector2 (Origin.x + expands.x, Origin.y - expands.y);

        public float Height => Mathf.Abs (BottomLeft.y - TopLeft.y);
        public float Width => Mathf.Abs (BottomLeft.x - BottomRight.x);

        public Vector2 Size {
            get { return expands; }
            set {
                expands = value;
            }
        }

        public ActorBounds (Vector2 offset, Vector2 expands) {
            this.offset = offset;
            this.expands = expands;
            Origin = Vector2.zero + offset;
        }

        public void SetOrigin (Vector3 position) {
            Origin = new Vector2 (position.x + offset.x, position.y + offset.y);
        }

        public void Expand (float amount) {
            expands = new Vector2 (expands.x + amount, expands.y + amount);          
        }
/*
        public static ActorBounds CreateFromBounds (Bounds bounds) {
            var actorBounds = new ActorBounds () {

            };
            return actorBounds;
        }
        */
#if UNITY_EDITOR
        public void DrawSceneGizmos () {
            var color = Gizmos.color;
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine (TopLeft, TopRight);
            Gizmos.DrawLine (TopRight, BottomRight);
            Gizmos.DrawLine (BottomRight, BottomLeft);
            Gizmos.DrawLine (BottomLeft, TopLeft);
            Gizmos.color = color;
        }
#endif
    }

}