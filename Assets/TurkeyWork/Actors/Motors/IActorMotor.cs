using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    public interface IActorMotor {

        MotorState Move (Vector3 velocity);
    }

}
