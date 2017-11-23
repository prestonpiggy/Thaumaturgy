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

        MotorState state;

        // Methods in this class assing to this variable.
        Vector3 velocity;
        CollisionInfo collisions;

        ActorBody parentActor;

        public Vector3 Velocity => velocity;
        public MotorState State => state;
        public bool OnGround => state.CollisionState.HasFlag (CollisionInfo.Below);

        void Awake () {
            parentActor = GetComponent<ActorBody> ();
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector3 deltaPosition) {
            state.ResetAll ();

            collisions = CollisionInfo.None;
            velocity = deltaPosition;

            CheckHorizontal ();
            CheckVertical ();

            state.Velocity = velocity;
            state.CollisionState = collisions;
            transform.Translate (velocity);
            return state;
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
            rayOrigin.y += StepHeight;
            for (var i = 0; i < horizontalRayCount; i++) {
#if DEBUG
                Debug.DrawRay (rayOrigin, rayDir * rayLength * 10, Color.yellow);
#endif
                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);
                rayOrigin.y += horizontalRaySpacing;

                if (!rayHit)
                    continue;
               
                // Propbably implement some slopstuff here?

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
#if DEBUG
                Debug.DrawRay (rayOrigin, rayDir * rayLength * 10, Color.yellow);
#endif
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

        // Wurks?
        void ClimpSlope (float surfaceAngle) {
            state.SurfaceAngle = surfaceAngle;

            var climbVelocityY = Mathf.Sin (surfaceAngle * Mathf.Deg2Rad) * Mathf.Abs (velocity.x);

            if (state.Velocity.y <= climbVelocityY) {
                velocity = new Vector3 (climbVelocityY, Mathf.Cos (surfaceAngle * Mathf.Deg2Rad) * velocity.x);
                collisions |= CollisionInfo.Below ^ CollisionInfo.OnSlope;
            }
        }

        void UpdateCollisionRaySpacing () {
            var bounds = parentActor.Bounds;
            var boundsWidth = bounds.Width + SKIN_WIDTH * -2;
            var boundsHeight = bounds.Height + SKIN_WIDTH * -2 - StepHeight;

            if (boundsHeight < StepHeight)
                Debug.LogWarning ("Actors collision volume is very small. This may lead to inaccurate collision detection!");

            horizontalRayCount = Mathf.RoundToInt (boundsHeight * raysPerWorldUnit);
            verticalRayCount = Mathf.RoundToInt (boundsWidth * raysPerWorldUnit);
            horizontalRaySpacing = bounds.Height / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.Width / (verticalRayCount - 1);
        }
    }

}
