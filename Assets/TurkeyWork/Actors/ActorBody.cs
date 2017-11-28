using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Actors {

    public class ActorBody : MonoBehaviour {

        public Bounds Bounds => Collider.bounds;

        [Required, CustomContextMenu ("Find", "FindCollider")]
        public BoxCollider2D Collider;

        public bool HasCollider => Collider != null;

        void FindCollider () {
            Collider = GetComponent<BoxCollider2D> ();
        }

    }

}
