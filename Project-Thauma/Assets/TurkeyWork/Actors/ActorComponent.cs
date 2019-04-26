using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Actors {

    [RequireComponent (typeof (ActorBody))]
    public class ActorComponent : MonoBehaviour {

        private ActorBody parentActor;
        public ActorBody ParentActor => parentActor;

        protected virtual void Awake () {
            parentActor = GetComponent<ActorBody> ();
        }

    }

}