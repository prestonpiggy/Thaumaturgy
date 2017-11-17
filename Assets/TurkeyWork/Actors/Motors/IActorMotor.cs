using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public interface IActorMotor {

        Vector3 Velocity { get; }

        MotorState Move (Vector3 velocity);
    }

}
