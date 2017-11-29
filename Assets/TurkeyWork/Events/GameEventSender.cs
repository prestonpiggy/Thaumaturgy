using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.Events {

    public class GameEventSender : MonoBehaviour {

        [AssetsOnly]
        public GameEvent EventToSend;

        public void SendEvent () {
            EventToSend.Raise ();
        }
    }

}