using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public interface IActorMotor {

        Vector3 MovementDelta { get; }
        Vector3 Velocity { get; }

        MotorState State { get; }

        MotorState Move (Vector3 velocity, float deltaTime);
    }

}
