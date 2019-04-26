using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Actors {

    public class ActorAnimator : MonoBehaviour {

        [CustomContextMenu ("Find", "FindAnimator"), Required]
        public Animator animator;
        ActorController controller;

        private void Awake () {
            controller = GetComponent<ActorController> ();
        }

        private void LateUpdate () {
            animator.SetBool ("Face Right", controller.FacesRight);
            animator.SetFloat ("Velocity Horizontal", controller.Velocity.x);
            animator.SetFloat ("Velocity Vertical", controller.Velocity.y);
        }

        void FindAnimator () {
            animator = GetComponentInChildren<Animator> ();
        }

    }

}
