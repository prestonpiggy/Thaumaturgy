using System.Collections;
using System.Collections.Generic;
using Bolt;
using Sirenix.OdinInspector;
using UnityEngine;

using TurkeyWork.Actors;
using TurkeyWork.Abilities;
using TurkeyWork.Events;

namespace TurkeyWork.Actors {

    public class PlayerController : ActorController {

        [MinValue (0)]
        public float AccelerationTime = 2f;
        [Range (0, 1)]
        public float AirControl = 0.7f;
        [MinValue (0)]
        public byte MaxJumps = 1;
        [Range (0f, 1f)] public float JumpFallOff = 0.9f;

        float maxJumpVelocity;
        float minJumpVelocity;
        byte currentMultiJump;

        public ActorAttributes Attributes { get; protected set; }

        ICmdPlayerMovementInput input;
        CmdPlayerMovement inputCommand;

        Vector3 frameVelocity;
        MotorState motorState;

        float currentHorizontal;
        bool abilityOverride;

        Vector2 directionInput;
        bool jumpFlag;

        // Shit implementation. For now.
        [System.NonSerialized] public bool OnLadder;

        protected override void Awake () {
            base.Awake ();
            Motor = GetComponent<IActorMotor> ();
            Attributes = GetComponent<ActorAttributes> ();
            RecalculateJump ();
            enabled = false;
        }

        public override void Attached () {
            enabled = true;
            state.SetTransforms (state.ActorTransform, transform);
            RecalculateJump ();
        }

        private void Update () {
            if (directionInput == Vector2.zero)
                directionInput = new Vector3 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
            if (jumpFlag == false)
                jumpFlag = Input.GetKeyDown (KeyCode.Space);
        }

        public override void SimulateController () {
            input = CmdPlayerMovement.Create ();
            input.Direction = directionInput;
            input.JumpFlag = jumpFlag;

            entity.QueueInput (input);

            ResetInputs ();
        }

        public override void ExecuteCommand (Command command, bool resetState) {
            if (!command.IsFirstExecution)
                return;

            inputCommand = command as CmdPlayerMovement;

            if (inputCommand && !resetState) {
                UpdateFrameVelocity ();

                motorState = Motor.Move (frameVelocity, BoltNetwork.frameDeltaTime);

                if (motorState.CollidingAboveOrBelow) {
                    frameVelocity.y = 0;
                }

                inputCommand.Result.Velocity = motorState.Velocity;
            }
        }

        public void SetPosition (Vector3 position) {
            if (entity.isOwner)
                transform.position = position;
        }

        void UpdateFrameVelocity () {
            frameVelocity = new Vector3 (
                        GetHorizontalMove (),
                        GetVerticalMove ()
                        );
            CheckJump ();
        }

        float GetHorizontalMove () {
            var effectiveAcceleration = motorState.OnGround ? AccelerationTime : AccelerationTime / AirControl;
            return Mathf.SmoothDamp (
                            frameVelocity.x,
                            inputCommand.Input.Direction.x * Attributes.MovementSpeed.Value001,
                            ref currentHorizontal,
                            effectiveAcceleration
                            );
        }

        float GetVerticalMove () {
            return OnLadder ?
                inputCommand.Input.Direction.y * Attributes.MovementSpeed.Value001 * 0.7f
                :
                frameVelocity.y + Physics2D.gravity.y * Attributes.GravityScale.Value001 * BoltNetwork.frameDeltaTime;
        }

        void CheckJump () {
            if (motorState.OnGround) {
                if (inputCommand.Input.JumpFlag) {
                    if (currentMultiJump == 0) {
                        frameVelocity.y = maxJumpVelocity;
                        currentMultiJump++;
                    }
                } else {
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
            var gravityY = Physics2D.gravity.y * Attributes.GravityScale.Value001;
            var gravitySign = Mathf.Sign (gravityY);
            var absGravity = gravitySign * gravityY;
            var timeToApex = Mathf.Sqrt (2 * Attributes.JumpHeight.Value001 / absGravity);

            maxJumpVelocity = absGravity * timeToApex;
            minJumpVelocity = -gravitySign * Mathf.Sqrt (2 * gravitySign * gravityY * timeToApex);
        }

        void ResetInputs () {
            directionInput = Vector2.zero;
            jumpFlag = false;
        }
    }

}