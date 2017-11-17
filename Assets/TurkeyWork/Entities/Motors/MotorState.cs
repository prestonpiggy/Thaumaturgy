using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [System.Flags]
    public enum CollisionFlag : byte { None = 0, Below = 1, Above = 2, Left = 4, Right = 8, Front = 16, Back = 32 }

    public struct MotorState {
        public Vector3 Velocity;
        public CollisionFlag CollisionFlags;

        public void ResetAll () {
            Velocity = Vector3.zero;
            ResetCollisions ();
        }

        public void ResetCollisions () {
            CollisionFlags = 0;
        }
    }

}