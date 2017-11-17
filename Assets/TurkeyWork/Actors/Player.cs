using System.Collections;
using System.Collections.Generic;
using Bolt;
using UnityEngine;

using TurkeyWork.Actors;

public class Player : EntityEventListener<IPlayerState> {

    public float Speed = 4f;
    public float Smoothing = 2f;
    public float JumpHeight = 2.4f;
    public float GravityScale = 3f;
    public byte MaxMultiJump = 1;
    [Range (0f, 1f)] public float JumpFallOff = 0.9f;

    float maxJumpVelocity;
    float minJumpVelocity;
    byte currentMultiJump;

    ActorMotor2D motor;

    ICmdPlayerMovementInput input;
    CmdPlayerMovement inputCommand;

    Vector3 frameVelocity;
    float currentHorizontal;

    private void Awake () {
        motor = GetComponent<ActorMotor2D> ();
        RecalculateJump ();
    }

    public override void Attached () {
        state.SetTransforms (state.PlayerTransform, transform);
    }

    public override void SimulateController () {
        input = CmdPlayerMovement.Create ();
        input.Horizontal = Input.GetAxisRaw ("Horizontal");
        input.JumpFlag = Input.GetKeyDown (KeyCode.Space);
        entity.QueueInput (input);
    }

    public override void ExecuteCommand (Command command, bool resetState) {
        if (!command.IsFirstExecution)
            return;

        inputCommand = command as CmdPlayerMovement;
        
        if (inputCommand) {
            if (resetState) {
                
            }
            else {
                // Somehow this broke? Even when this is the exat code i use in other project. There, it works fine!
                var x = Mathf.SmoothDamp (motor.Velocity.x, inputCommand.Input.Horizontal * Speed,
                        ref currentHorizontal, Smoothing);
                frameVelocity = new Vector3 (
                    inputCommand.Input.Horizontal * Speed,
                    motor.Velocity.y + Physics2D.gravity.y * BoltNetwork.frameDeltaTime * GravityScale
                    );
                CheckJump ();
                motor.Move (frameVelocity * BoltNetwork.frameDeltaTime);
            }
        }
    }
        
    public void SetPosition (Vector3 position) {
        if (entity.isOwner)
            transform.position = position;
    }
    
    void CheckJump () {
        if (motor.OnGround) {
            if (inputCommand.Input.JumpFlag) {
                if (currentMultiJump == 0) {
                    frameVelocity.y = maxJumpVelocity;
                    currentMultiJump++;
                } 
            }
            else {
                currentMultiJump = 0;
            }
        } else if (inputCommand.Input.JumpFlag && currentMultiJump <= MaxMultiJump) {
            frameVelocity.y = Mathf.Clamp (maxJumpVelocity, minJumpVelocity, frameVelocity.y + GetMultiJumpStrength (currentMultiJump));
            currentMultiJump++;
        }
    }
    
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

