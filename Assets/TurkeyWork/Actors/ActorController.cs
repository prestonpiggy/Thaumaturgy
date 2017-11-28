using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace TurkeyWork.Actors {

    public abstract class ActorController : EntityEventListener<IActorState> {

        private ActorBody parentActor;
        public ActorBody ParentActor => parentActor;

        public IActorMotor Motor { get; protected set; }
        public Vector3 Velocity { get; protected set; }

        public bool FacesRight => true;

        protected virtual void Awake () {
            parentActor = GetComponent<ActorBody> ();
        }

    }
}
