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
        Bounds actorBounds;

        // Methods in this class assing to this variable.
        Vector3 movementDelta;
        float deltaTime;

        CollisionInfo collisions;

        ActorBody parentActor;

        public Vector3 MovementDelta => movementDelta;
        public Vector3 Velocity => movementDelta / deltaTime;

        public MotorState State => state;
        public bool OnGround => state.CollisionState.HasFlag (CollisionInfo.Below);

        void Awake () {
            parentActor = GetComponent<ActorBody> ();
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector3 velocity, float deltaTime) {
            UpdateCollisionRaySpacing ();
            state.ResetAll ();

            this.deltaTime = deltaTime;
            collisions = CollisionInfo.None;
            movementDelta = velocity * deltaTime;

            CheckHorizontal ();
            CheckVertical ();

            transform.Translate (movementDelta);

            state.Velocity = movementDelta / deltaTime;
            state.CollisionState = collisions;
            return state;
        }

        void CheckHorizontal () {
            var moveX = movementDelta.x;
            var rayLength = Mathf.Abs(moveX) + SKIN_WIDTH;

            var directionX = Mathf.Sign (moveX);
            var rayDir = directionX * Vector2.right;

            var collisionFlag = directionX == 1 ? CollisionInfo.Right : CollisionInfo.Left;
            var rayOrigin = directionX == 1 ? actorBounds.BottomRight () : actorBounds.BottomLeft ();
            rayOrigin.y += StepHeight;
            for (var i = 0; i < horizontalRayCount; i++) {
#if DEBUG
                Debug.DrawRay (rayOrigin, rayDir * rayLength, Color.yellow);
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
            movementDelta.x =  moveX;
        }

        void CheckVertical () {
            var moveY = movementDelta.y;
            var rayLength = Mathf.Abs (moveY) + SKIN_WIDTH + StepHeight;

            var directionY = Mathf.Sign (moveY);
            var rayDir = directionY * Vector2.up;

            var collisionFlag = directionY == 1 ? CollisionInfo.Above : CollisionInfo.Below;
            var rayOrigin = directionY == 1 ? actorBounds.TopLeft () : actorBounds.BottomLeft () + Vector3.up * StepHeight;

            for (var i = 0; i < verticalRayCount; i++) {
#if DEBUG
                Debug.DrawRay (rayOrigin, rayDir * rayLength, Color.yellow);
#endif
                var rayHit = Physics2D.Raycast (rayOrigin, rayDir, rayLength, CollisionMask);
                rayOrigin.x += verticalRaySpacing;

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                moveY = (rayLength - SKIN_WIDTH - StepHeight) * directionY;
                collisions |= collisionFlag;
            }
            movementDelta.y = moveY;
        }

        // Wurks? Well, not used :D
        void ClimpSlope (float surfaceAngle) {
            state.SurfaceAngle = surfaceAngle;

            var climbVelocityY = Mathf.Sin (surfaceAngle * Mathf.Deg2Rad) * Mathf.Abs (movementDelta.x);

            if (state.Velocity.y <= climbVelocityY) {
                movementDelta = new Vector3 (climbVelocityY, Mathf.Cos (surfaceAngle * Mathf.Deg2Rad) * movementDelta.x);
                collisions |= CollisionInfo.Below ^ CollisionInfo.OnSlope;
            }
        }

        void UpdateCollisionRaySpacing () {
            actorBounds = parentActor.Bounds;
            actorBounds.Expand (SKIN_WIDTH * -2);

            var boundsWidth = actorBounds.size.x;
            var boundsHeight = actorBounds.size.y - StepHeight;

            if (boundsHeight < StepHeight)
                Debug.LogWarning ("Actors collision volume is very small. This may lead to inaccurate collision detection!");

            horizontalRayCount = Mathf.RoundToInt (boundsHeight * raysPerWorldUnit);
            verticalRayCount = Mathf.RoundToInt (boundsWidth * raysPerWorldUnit);
            horizontalRaySpacing = boundsWidth / (horizontalRayCount - 1);
            verticalRaySpacing = boundsHeight / (verticalRayCount - 1);
        }
    }

}
