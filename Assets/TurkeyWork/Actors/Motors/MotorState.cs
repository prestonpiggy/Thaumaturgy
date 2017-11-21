using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Flags]
    public enum CollisionSide : byte { None = 0, Below = 1, Above = 2, Left = 4, Right = 8, Front = 16, Back = 32 }

    public struct MotorState {
        public Vector3 Velocity;
        public float SlopeAngle, SlopeAnglePrevious;
        public CollisionSide CollisionFlags;

        public bool OnGround => CollisionFlags.HasFlag (CollisionSide.Below);
        public bool CollidingAboveOrBelow => CollisionFlags.HasFlag (CollisionSide.Below) || CollisionFlags.HasFlag (CollisionSide.Above);

        public void ResetAll () {
            Velocity = Vector3.zero;
            SlopeAnglePrevious = SlopeAngle;
            SlopeAngle = 0f;
            ResetCollisions ();
        }

        public void ResetCollisions () {
            Debug.Log ("Reset?");
            CollisionFlags = 0;
        }
    }

}