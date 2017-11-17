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
        [SerializeField, Range (1, 20)] byte raysPerUnit = 10;

        int horizontalRayCount;
        int verticalRayCount;

        float horizontalRaySpacing;
        float verticalRaySpacing;

        MotorState state;
        Vector3 velocity;

        ActorBody parentActor;

        public Vector3 Velocity => velocity;
        public MotorState State => state;

        void Awake () {
            UpdateCollisionRaySpacing ();
        }

        public MotorState Move (Vector2 velocity) {
            return Move ((Vector3) velocity);
        }

        public MotorState Move (Vector3 velocity) {
            var vy = CheckVertical (velocity.y);
            var vx = CheckHorizontal (velocity.x);
            state.Velocity = new Vector3 (vx, vy);
            return state;
        }

        float CheckHorizontal (float velocity) {
            var direction = Mathf.Sign (velocity);
            var rayLength = direction * velocity + SKIN_WIDTH;
            var rayDirection = direction * Vector2.right;
            var bounds = parentActor.Bounds;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var collisionFlag = direction == 1 ? CollisionFlag.Right : CollisionFlag.Left;
            var rayOrigin = direction == 1 ? bounds.BottomRight : bounds.BottomLeft;

            if (CheckForWalkableSlopes (rayOrigin, rayDirection, rayLength)) {
                print ("There is a walkable slope ahead!");
            }

            for (var i = 0; i < horizontalRayCount; i++) {
                Debug.DrawRay (rayOrigin, rayDirection * rayLength, Color.cyan);

                var rayHit = Physics2D.Raycast (rayOrigin, rayDirection, rayLength, CollisionMask);
                rayOrigin.y += horizontalRaySpacing;

                if (!rayHit)
                    continue;           

                rayLength = rayHit.distance;
                state.CollisionFlags |= collisionFlag;
            }
            velocity = (rayLength - SKIN_WIDTH) * direction;
            return velocity;
        }

        float CheckVertical (float velocity) {
            var direction = Mathf.Sign (velocity);
            var rayLength = direction * velocity + SKIN_WIDTH;
            var rayDirection = direction * Vector2.up;
            var bounds = parentActor.Bounds;

            if (rayLength < 2 * SKIN_WIDTH)
                rayLength = 2 * SKIN_WIDTH;

            var collisionFlag = direction == 1 ? CollisionFlag.Above : CollisionFlag.Below;
            var rayOrigin = direction == 1 ? bounds.TopLeft : bounds.BottomLeft;

            for (var i = 0; i < verticalRayCount; i++) {
                Debug.DrawRay (rayOrigin, rayDirection * rayLength, Color.cyan);

                var rayHit = Physics2D.Raycast (rayOrigin, rayDirection, rayLength, CollisionMask);
                rayOrigin.x += verticalRaySpacing;

                if (!rayHit)
                    continue;

                rayLength = rayHit.distance;
                state.CollisionFlags |= collisionFlag;
            }
            velocity = (rayLength - SKIN_WIDTH) * direction;
            return velocity;
        }

        bool CheckForWalkableSlopes (Vector3 origin, Vector3 direction, float length) {
            var rayHit = Physics2D.Raycast (origin, direction, length, CollisionMask);
            var angle = Vector3.Angle (direction, rayHit.normal);

            if (angle > 0 && angle < MaxSlopeAngle) {
                print (angle);
                return true;
            }
            return false;
        }

        void HandleSlopes () {

        }

        void UpdateCollisionRaySpacing () {
            var bounds = parentActor.Bounds;
            var boundsWidth = bounds.Width + SKIN_WIDTH * -2;
            var boundsHeight = bounds.Height + SKIN_WIDTH * -2;

            horizontalRayCount = Mathf.RoundToInt (boundsHeight / raysPerUnit);
            verticalRayCount = Mathf.RoundToInt (boundsWidth / raysPerUnit);
            horizontalRaySpacing = bounds.Height / (horizontalRayCount - 1);
            verticalRaySpacing = bounds.Width / (verticalRayCount - 1);
        }
    }

}
