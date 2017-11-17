using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Flags]
    public enum CollisionSides : byte { None = 0, Below = 1, Above = 2, Left = 4, Right = 8, Front = 16, Back = 32 }

    public struct MotorState {
        public Vector3 Velocity;
        public float SlopeAngle, SlopeAnglePrevious;
        public CollisionSides CollisionFlags;

        public void ResetAll () {
            Velocity = Vector3.zero;
            SlopeAnglePrevious = SlopeAngle;
            SlopeAngle = 0f;
            ResetCollisions ();
        }

        public void ResetCollisions () {
            CollisionFlags = 0;
        }
    }

}