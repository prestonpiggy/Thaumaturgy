using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

using TurkeyWork.Events;

namespace TurkeyWork.Networking {

    [BoltGlobalBehaviour (BoltNetworkModes.Host)]
    public class ServerCallbacks : GlobalEventListener {

        void Awake () {
            NetworkManager.CreateServerPlayer ();
        }

        public override void Connected (BoltConnection connection) {
            NetworkManager.CreateClientPlayer (connection);
        }

        public override void SceneLoadLocalDone (string map) {
            //SpawnPlayer (null);
        }

        public override void SceneLoadRemoteDone (BoltConnection connection) {
            //SpawnPlayer (connection);
        }

    }
}
