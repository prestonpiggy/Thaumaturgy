using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Flags]
    public enum CollisionInfo : byte { None = 0, Below = 1, Above = 2, Left = 4, Right = 8, Front = 16, Back = 32, OnSlope = 64 }

    public struct MotorState {
        public Vector3 Velocity;
        public float SurfaceAngle, SurfaceAngleOld;
        public CollisionInfo CollisionState;

        public bool ClimbingSlope => CollisionState.HasFlag (CollisionInfo.OnSlope);
        public bool OnGround => CollisionState.HasFlag (CollisionInfo.Below);
        public bool CollidingAboveOrBelow => CollisionState.HasFlag (CollisionInfo.Below) || CollisionState.HasFlag (CollisionInfo.Above);

        public void ResetAll () {
            Velocity = Vector3.zero;
            SurfaceAngleOld = SurfaceAngle;
            SurfaceAngle = 0f;
            ResetCollisions ();
        }

        public void ResetCollisions () {
            Debug.Log ("Reset?");
            CollisionState = 0;
        }
    }

}