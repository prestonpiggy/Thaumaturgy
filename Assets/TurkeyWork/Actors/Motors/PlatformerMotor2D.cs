using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public class PlatformerMotor2D : MonoBehaviour, IActorMotor {

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

        MotorState motorState;

        // Methods in this class assing to this variable.
        Vector3 velocity;
        CollisionInfo collisions;

        ActorBody parentActor;

        public bool OnGround => motorState.CollisionState.HasFlag (CollisionInfo.Below);

        void Awake () {
            parentActor = GetComponent<ActorBody> ();
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector3 deltaPosition) {
            collisions = CollisionInfo.None;
            velocity = deltaPosition;

            CheckHorizontal ();
            CheckVertical ();

            motorState.ResetAll ();
            motorState.Velocity = velocity;
            motorState.CollisionState = collisions;
            transform.Translate (velocity);
            return motorState;
        }

        void CheckHorizontal () {
            var moveX = velocity.x;
            var rayLength = Mathf.Abs(moveX) + SKIN_WIDTH;

            //if (rayLength < 2 * SKIN_WIDTH)
            //    rayLength = 2 * SKIN_WIDTH;

            var directionX = Mathf.Sign (moveX);
            var rayDir = directionX * Vector2.right;

            var collisionFlag = directionX == 1 ? CollisionInfo.Right : CollisionInfo.Left;
            var rayOrigin = directionX == 1 ? parentActor.Bounds.BottomRight : parentActor.Bounds.BottomLeft;

            for (var i = 0; i < horizontalRayCount; i++) {
                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);
                rayOrigin.y += horizontalRaySpacing;

                if (!rayHit)
                    continue;
                /*
                var surfaceAngle = Vector2.Angle (rayHit.normal, Vector2.up);

                if (i == 0 && surfaceAngle <= MaxSlopeAngle) {
                    print (surfaceAngle);

                    var distanceToSlope = 0f;

                    if (surfaceAngle != motorState.SurfaceAngleOld) {
                        distanceToSlope = rayHit.distance - SKIN_WIDTH;
                        velocity.x = moveX -= distanceToSlope * directionX;
                    }
                    ClimpSlope (surfaceAngle);
                    moveX += distanceToSlope * directionX;
                }
                
                if (collisions.HasFlag (CollisionInfo.OnSlope) || surfaceAngle > MaxSlopeAngle)
                    continue;
*/
                rayLength = rayHit.distance;
                moveX = (rayLength - SKIN_WIDTH) * directionX;

                collisions |= collisionFlag;
            }
            velocity.x =  moveX;
        }

        void CheckVertical () {
            var moveY = velocity.y;
            var rayLength = Mathf.Abs (moveY) + SKIN_WIDTH;

            //if (rayLength < 2 * SKIN_WIDTH)
            //    rayLength = 2 * SKIN_WIDTH;

            var directionY = Mathf.Sign (moveY);
            var rayDir = directionY * Vector2.up;

            var collisionFlag = directionY == 1 ? CollisionInfo.Above : CollisionInfo.Below;
            var rayOrigin = directionY == 1 ? parentActor.Bounds.TopLeft : parentActor.Bounds.BottomLeft;

            for (var i = 0; i < verticalRayCount; i++) {
                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);
                rayOrigin.x += verticalRaySpacing;

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                moveY = (rayLength - SKIN_WIDTH) * directionY;
                collisions |= collisionFlag;
            }
            velocity.y = moveY;
        }

        void ClimpSlope (float surfaceAngle) {
            motorState.SurfaceAngle = surfaceAngle;

            var climbVelocityY = Mathf.Sin (surfaceAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x);

            if (motorState.Velocity.y <= climbVelocityY) {
                velocity = new Vector3 (climbVelocityY, Mathf.Cos (surfaceAngle * Mathf.Deg2Rad) * velocity.x);
                collisions |= CollisionInfo.Below ^ CollisionInfo.OnSlope;
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
