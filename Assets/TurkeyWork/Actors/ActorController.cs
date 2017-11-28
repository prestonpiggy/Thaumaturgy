using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

namespace TurkeyWork.Actors {

    public abstract class ActorController : EntityEventListener<IActorState> {

        private ActorBody parentActor;
        public ActorBody ParentActor => parentActor;

        protected virtual void Awake () {
            parentActor = GetComponent<ActorBody> ();
        }

    }
}
