using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public class ActorMotor2D : MonoBehaviour, IActorMotor {

        public const float SKIN_WIDTH = .01f;

        public float StepHeight = 0.2f;
        public float MaxSlopeAngle = 60f;

        [Header ("Collision Detection")]
        public LayerMask CollisionMask;

        // the actual ray count is this plus one
        [SerializeField, Range (1, 20)] byte raysPerWorldUnit = 10;

        int horizontalRayCount;
        int verticalRayCount;

        float horizontalRaySpacing;
        float verticalRaySpacing;

        MotorState state;
        Vector3 velocity;

        ActorBody parentActor;

        public Vector3 Velocity => velocity;
        public MotorState State => state;

        public bool OnGround => state.CollisionFlags.HasFlag (CollisionSides.Below);

        void Awake () {
            parentActor = GetComponent<ActorBody> ();
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector3 deltaPosition) {
            print ("Moving" + deltaPosition);
            var vy = CheckVertical (deltaPosition.y);
            var vx = CheckHorizontal (deltaPosition.x);
            state.Velocity = new Vector3 (vx, vy);
            return state;
        }

        float CheckHorizontal (float deltaPosition) {
            var direction = Mathf.Sign (deltaPosition);
            var rayLength = direction * deltaPosition + SKIN_WIDTH;
            var rayDirection = direction * Vector2.right;
            var bounds = parentActor.Bounds;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var collisionFlag = direction == 1 ? CollisionSides.Right : CollisionSides.Left;
            var rayOrigin = direction == 1 ? bounds.BottomRight : bounds.BottomLeft;
            float angle;

            if (CheckForWalkableSlopes (rayOrigin, rayDirection, rayLength, out angle)) {
                HandleSlopes (angle, deltaPosition * direction);
            }

            for (var i = 0; i < horizontalRayCount; i++) {
                var rayHit = Physics2D.Raycast (rayOrigin, rayDirection, rayLength, CollisionMask);
                rayOrigin.y += horizontalRaySpacing;

                if (!rayHit)
                    continue;           

                rayLength = rayHit.distance;
                state.CollisionFlags |= collisionFlag;
            }
            deltaPosition = (rayLength - SKIN_WIDTH) * direction;
            return deltaPosition;
        }

        float CheckVertical (float deltaPosition) {
            var direction = Mathf.Sign (deltaPosition);
            var rayLength = direction * deltaPosition + SKIN_WIDTH;
            var rayDirection = direction * Vector2.up;
            var bounds = parentActor.Bounds;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var collisionFlag = direction == 1 ? CollisionSides.Above : CollisionSides.Below;
            var rayOrigin = direction == 1 ? bounds.TopLeft : bounds.BottomLeft;

            for (var i = 0; i < verticalRayCount; i++) {
                var rayHit = Physics2D.Raycast (rayOrigin, rayDirection, rayLength, CollisionMask);
                rayOrigin.x += verticalRaySpacing;

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                state.CollisionFlags |= collisionFlag;
            }
            deltaPosition = (rayLength - SKIN_WIDTH) * direction;
            return deltaPosition;
        }

        bool CheckForWalkableSlopes (Vector3 origin, Vector3 direction, float length, out float angle) {
            var rayHit = Physics2D.Raycast (origin, direction, length, CollisionMask);
            angle = Vector3.Angle (direction, rayHit.normal);

            if (angle > 0 && angle < MaxSlopeAngle) {
                print (angle);
                return true;
            }
            return false;
        }

        void HandleSlopes (float angle, float distance) {
            print ("Handling slope");
            var climbVelocityY = Mathf.Sin (angle * Mathf.Deg2Rad) * distance;

            if (state.Velocity.y <= climbVelocityY) {
                velocity = new Vector3 (climbVelocityY, Mathf.Cos (angle * Mathf.Deg2Rad) * distance);
                state.CollisionFlags |= CollisionSides.Below;
                state.SlopeAngle = angle;
            }
        }

        void UpdateCollisionRaySpacing () {
            var bounds = parentActor.Bounds;
            var boundsWidth = bounds.Width + SKIN_WIDTH * -2;
            var boundsHeight = bounds.Height + SKIN_WIDTH * -2;

            horizontalRayCount = Mathf.RoundToInt (boundsHeight * raysPerWorldUnit);
            verticalRayCount = Mathf.RoundToInt (boundsWidth * raysPerWorldUnit);
            horizontalRaySpacing = bounds.Height / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.Width / (verticalRayCount - 1);
        }
    }

}
