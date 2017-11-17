using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Serializable]
    public struct ActorBounds {
        public Vector2 TopLeft, TopRight;
        public Vector2 BottomLeft, BottomRight;

        public float Height => Mathf.Abs (BottomLeft.y - TopLeft.y);
        public float Width => Mathf.Abs (BottomLeft.x - BottomRight.x);

        public ActorBounds (Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft) {
            TopLeft = topLeft;
            TopRight = topRight;
            BottomRight = bottomRight;
            BottomLeft = bottomLeft;
        }

        public ActorBounds CenterOnPosition (Vector3 position) {
            var actorBounds = new ActorBounds () {
                TopLeft = new Vector2 (position.x + TopLeft.x, position.y + TopLeft.y),
                TopRight = new Vector2 (position.x + TopRight.x, position.y + TopRight.y),
                BottomRight = new Vector2 (position.x + BottomRight.x, position.y + BottomRight.y),
                BottomLeft = new Vector2 (position.x + BottomLeft.x, position.y + BottomLeft.y)
            };
            return actorBounds;
        }

        public void Expand (float amount) {
            TopLeft = new Vector2 (TopLeft.x + amount, TopLeft.y + amount);
            TopRight = new Vector2 (TopRight.x + amount, TopRight.y + amount);
            BottomRight = new Vector2 (BottomRight.x + amount, BottomRight.y + amount);
            BottomLeft = new Vector2 (BottomLeft.x + amount, BottomLeft.y + amount);
        }

        public static ActorBounds CreateFromBounds (Bounds bounds) {
            var actorBounds = new ActorBounds () {
                TopLeft = new Vector2 (bounds.min.x, bounds.max.y),
                TopRight = new Vector2 (bounds.max.x, bounds.max.y),
                BottomRight = new Vector2 (bounds.max.x, bounds.min.y),
                BottomLeft = new Vector2 (bounds.min.x, bounds.min.y)
            };
            return actorBounds;
        }

#if UNITY_EDITOR
        public void DrawSceneGizmos () {
            Gizmos.DrawLine (TopLeft, TopRight);
            Gizmos.DrawLine (TopRight, BottomRight);
            Gizmos.DrawLine (BottomRight, BottomLeft);
            Gizmos.DrawLine (BottomLeft, TopLeft);
        }
#endif
    }

}