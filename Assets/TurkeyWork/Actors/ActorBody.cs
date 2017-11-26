using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Actors {

    public class ActorBody : MonoBehaviour {

        [SerializeField, HideIf ("HasCollider")] Bounds actorBounds = new Bounds (
            new Vector2 (0, 1), new Vector2 (1, 2)
            );
        public Bounds Bounds => Collider ? Collider.bounds : actorBounds;

        public BoxCollider2D Collider;

        public bool HasCollider => Collider != null;

        

    }

}
