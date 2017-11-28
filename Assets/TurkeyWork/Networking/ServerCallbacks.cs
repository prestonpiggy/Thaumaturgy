using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

using TurkeyWork.Events;

namespace TurkeyWork.Networking {

    [BoltGlobalBehaviour (BoltNetworkModes.Host)]
    public class ServerCallbacks : GlobalEventListener {

        public GameEvent LocalPlayerCreatedEvent;

        void Awake () {
            NetworkManager.CreateServerPlayer ();
        }

        public override void Connected (BoltConnection connection) {
            NetworkManager.CreateClientPlayer (connection);
        }

        public override void SceneLoadLocalDone (string map) {
            SpawnPlayer (null);
        }

        public override void SceneLoadRemoteDone (BoltConnection connection) {
            SpawnPlayer (connection);
        }

        void SpawnPlayer (BoltConnection connection) {
            if (NetworkManager.InGame) {
                var np = NetworkManager.GetPlayer (connection);
                np.SpawnPlayer ();
                if (np.IsLocal) {
                    if (LocalPlayerCreatedEvent != null)
                        LocalPlayerCreatedEvent.Raise ();
                    else
                        GameEvent.RaiseEvent ("Local Player Created");
                }
            }
        }

    }
}
