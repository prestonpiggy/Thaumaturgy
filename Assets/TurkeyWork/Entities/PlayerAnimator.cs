using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Player))]
public class PlayerAnimator : MonoBehaviour {

    Player player;
    Animator animator;
    CharacterController controller;

    private void Awake () {
        player = GetComponent<Player> ();
        animator = GetComponentInChildren<Animator> ();
        controller = GetComponent<CharacterController> ();
    }

    private void Update () {
        animator.SetFloat ("HorizontalSpeed", controller.velocity.x);
        animator.SetBool ("OnGround", controller.isGrounded);
    }

}
