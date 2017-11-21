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

        MotorState motorState;
        Vector3 velocity;
        CollisionSide collisions;

        ActorBody parentActor;

        public bool OnGround => motorState.CollisionFlags.HasFlag (CollisionSide.Below);

        void Awake () {
            parentActor = GetComponent<ActorBody> ();
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector3 deltaPosition) {
            collisions = CollisionSide.None;
            velocity = new Vector3 (CheckHorizontal(deltaPosition.x), CheckVertical (deltaPosition.y), 0f);
            motorState.Velocity = velocity;
            motorState.CollisionFlags = collisions;
            transform.Translate (velocity);
            return motorState;
        }

        float CheckHorizontal (float deltaPosition) {
            var moveX = deltaPosition;
            var rayLength = Mathf.Abs(moveX) + SKIN_WIDTH;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var directionX = Mathf.Sign (moveX);
            var rayDir = directionX * Vector2.right;

            var collisionFlag = directionX == 1 ? CollisionSide.Right : CollisionSide.Left;
            var rayOrigin = directionX == 1 ? parentActor.Bounds.BottomRight : parentActor.Bounds.BottomLeft;
            rayOrigin.y -= horizontalRaySpacing;

            for (var i = 0; i < horizontalRayCount; i++) {
                rayOrigin.y += horizontalRaySpacing;

                Debug.DrawRay (rayOrigin, rayDir * rayLength, Color.cyan);

                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                moveX = (rayLength - SKIN_WIDTH) * directionX;

                collisions |= collisionFlag;
            }
            return moveX;
        }

        float CheckVertical (float deltaPosition) {
            var moveY = deltaPosition;
            var rayLength = Mathf.Abs (moveY) + SKIN_WIDTH;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var directionY = Mathf.Sign (moveY);
            var rayDir = directionY * Vector2.up;

            var collisionFlag = directionY == 1 ? CollisionSide.Above : CollisionSide.Below;
            var rayOrigin = directionY == 1 ? parentActor.Bounds.TopLeft : parentActor.Bounds.BottomLeft;
            rayOrigin.x -= verticalRaySpacing;

            for (var i = 0; i < verticalRayCount; i++) {
                rayOrigin.x += verticalRaySpacing;

                Debug.DrawRay (rayOrigin, rayDir * rayLength, Color.cyan);

                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                moveY = (rayLength - SKIN_WIDTH) * directionY;
                collisions |= collisionFlag;
            }
            return moveY;
        }

        bool SlopeCheck (Vector3 origin, Vector3 direction, float length, out float angle) {
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

            if (motorState.Velocity.y <= climbVelocityY) {
                velocity = new Vector3 (climbVelocityY, Mathf.Cos (angle * Mathf.Deg2Rad) * distance);
                collisions |= CollisionSide.Below;
                motorState.SlopeAngle = angle;
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
