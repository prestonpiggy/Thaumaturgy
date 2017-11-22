using System.Collections;
using System.Collections.Generic;
using Bolt;
using Sirenix.OdinInspector;
using UnityEngine;

using TurkeyWork.Actors;
using TurkeyWork.Abilities;
using TurkeyWork.Events;

public class Player : EntityEventListener<IActorState> {

    [MinValue (0)]
    public float AccelerationTime = 2f;
    public float GravityScale = 3f;
    [Range(0, 1)]
    public float AirControl = 0.7f;
    [MinValue (0)]
    public byte MaxJumps = 1;
    [Range (0f, 1f)] public float JumpFallOff = 0.9f;

    float maxJumpVelocity;
    float minJumpVelocity;
    byte currentMultiJump;

    [AssetsOnly]
    public Ability TestAbility;
    private IEnumerator<AbilityInfo> abilityRoutine;

    public PlatformerMotor2D Motor { get; protected set; }
    public ActorBody ParentActor { get; protected set; }
    public ActorAttributes Attributes { get; protected set; }

    ICmdPlayerMovementInput input;
    CmdPlayerMovement inputCommand;

    Vector3 frameVelocity;
    Vector3 frameDeltaMove;

    float currentHorizontal;
    bool abilityOverride;

    private void Awake () {
        ParentActor = GetComponent<ActorBody> ();
        Motor = GetComponent<PlatformerMotor2D> ();
        Attributes = GetComponent<ActorAttributes> ();
        RecalculateJump ();
    }

    public override void Attached () {
        state.SetTransforms (state.ActorTransform, transform);
    }

    public override void ControlGained () {
        GameEvent.RaiseEvent ("Local Player Created");
    }

    public override void SimulateController () {
        if (abilityRoutine != null) {
               
            if (abilityOverride = abilityRoutine.Current.IsDone) {
                abilityRoutine = null;
            }
        }
        else if (Input.GetKeyDown (KeyCode.Mouse0)) {
            if (abilityRoutine == null) {
                abilityRoutine = TestAbility.Use (this);
                StartCoroutine (abilityRoutine);
            }         
        }

        input = CmdPlayerMovement.Create ();
        input.Horizontal = Input.GetAxisRaw ("Horizontal");
        input.JumpFlag = Input.GetKeyDown (KeyCode.Space);
        entity.QueueInput (input);      
    }

    MotorState motorState;
    public override void ExecuteCommand (Command command, bool resetState) {
        if (!command.IsFirstExecution)
            return;

        inputCommand = command as CmdPlayerMovement;
        
        if (inputCommand) {
            if (resetState) {
                
            }
            else {
                ParentActor.UpdateBounds ();
                UpdateFrameDeltaMove ();

                motorState = Motor.Move (frameDeltaMove);

                if (motorState.CollidingAboveOrBelow) {
                    frameVelocity.y = 0;
                }

               inputCommand.Result.Velocity = motorState.Velocity;
            }
        }
    }
        
    public void SetPosition (Vector3 position) {
        if (entity.isOwner)
            transform.position = position;
    }

    void UpdateFrameDeltaMove () {
        frameVelocity = new Vector3 (
                    GetHorizontalMove (),
                    frameVelocity.y + Physics2D.gravity.y * GravityScale * BoltNetwork.frameDeltaTime
                    );
        CheckJump ();
        frameDeltaMove = frameVelocity * BoltNetwork.frameDeltaTime; 
    }

    float GetHorizontalMove () {
        var effectiveAcceleration = Motor.OnGround ? AccelerationTime : AccelerationTime / AirControl;
        return Mathf.SmoothDamp (
                        frameVelocity.x,
                        inputCommand.Input.Horizontal * Attributes.MovementSpeed.Value001,
                        ref currentHorizontal, effectiveAcceleration
                        );
    }
    
    void CheckJump () {
        if (Motor.OnGround) {
            if (inputCommand.Input.JumpFlag) {
                if (currentMultiJump == 0) {
                    frameVelocity.y = maxJumpVelocity;
                    currentMultiJump++;
                } 
            }
            else {
                currentMultiJump = 0;
            }
        } else if (inputCommand.Input.JumpFlag && currentMultiJump < MaxJumps) {
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
        var timeToApex = Mathf.Sqrt (2 * Attributes.JumpHeight.Value001 / absGravity);

        maxJumpVelocity = absGravity * timeToApex;
        minJumpVelocity = -gravitySign * Mathf.Sqrt (2 * gravitySign * gravityY * timeToApex);
    }
}

