using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;


public class Player : MonoBehaviour { //EntityEventListener<IPlayerState> {

    public float Speed = 4f;
    public float Smoothing = 2f;
    public float JumpHeight = 2.4f;
    public float GravityScale = 3f;
    public byte MaxMultiJump = 1;
    [Range (0f, 1f)] public float JumpFallOff = 0.9f;

    float maxJumpVelocity;
    float minJumpVelocity;
    byte currentMultiJump;

    CharacterController controller;

    //IPlayerInputCommandInput input;
    //PlayerInputCommand inputCommand;

    Vector3 frameVelocity;
    float currentHorizontal;

    private void Awake () {
        controller = GetComponent<CharacterController> ();
        RecalculateJump ();
    }

    /*
    public override void Attached () {
        state.SetTransforms (state.Transform, transform);
    }

    public override void SimulateController () {
        input = PlayerInputCommand.Create ();
        input.Horizontal = Input.GetAxisRaw ("Horizontal");
        input.Jump = Input.GetKeyDown (KeyCode.Space);
        entity.QueueInput (input);
    }

    public override void ExecuteCommand (Command command, bool resetState) {
        inputCommand = command as PlayerInputCommand;
        if (!inputCommand.IsFirstExecution)
            return;
        if (inputCommand) {
            if (resetState) {
                
            }
            else {
                var x = Mathf.SmoothDamp (controller.velocity.x, inputCommand.Input.Horizontal * Speed,
                        ref currentHorizontal, Smoothing
                        );
                frameVelocity = new Vector3 (
                    x,
                    controller.velocity.y + Physics.gravity.y * BoltNetwork.frameDeltaTime * GravityScale
                    );
                CheckJump ();

                if (frameVelocity.x > 0)
                    transform.rotation = Quaternion.Euler (0, 90f, 0f);
                else if (frameVelocity.x < 0)
                    transform.rotation = Quaternion.Euler (0, -90f, 0f);

                controller.Move (frameVelocity * BoltNetwork.frameDeltaTime);
            }
        }
    }
    */

        
    public void SetPosition (Vector3 position) {
       // if (entity.isOwner)
         //   transform.position = position;
    }
    /*
    void CheckJump () {
        if (controller.isGrounded) {
            if (inputCommand.Input.Jump) {
                if (currentMultiJump == 0) {
                    frameVelocity.y = maxJumpVelocity;
                    currentMultiJump++;
                } 
            }
            else {
                currentMultiJump = 0;
            }
        } else if (inputCommand.Input.Jump && currentMultiJump <= MaxMultiJump) {
            frameVelocity.y = Mathf.Clamp (maxJumpVelocity, minJumpVelocity, frameVelocity.y + GetMultiJumpStrength (currentMultiJump));
            currentMultiJump++;
        }
    }
    */
    float GetMultiJumpStrength (byte multiJump) {
        return maxJumpVelocity * Mathf.Pow (JumpFallOff, multiJump);
    }

    void RecalculateJump () {
        var gravityY = Physics2D.gravity.y * GravityScale;
        var gravitySign = Mathf.Sign (gravityY);
        var absGravity = gravitySign * gravityY;
        var timeToApex = Mathf.Sqrt (2 * JumpHeight / absGravity);

        maxJumpVelocity = absGravity * timeToApex;
        minJumpVelocity = -gravitySign * Mathf.Sqrt (2 * gravitySign * gravityY * timeToApex);
    }
}

