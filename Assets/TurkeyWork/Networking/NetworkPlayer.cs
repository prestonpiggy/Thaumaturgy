using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TurkeyWork.World;

namespace TurkeyWork.Networking {

    public class NetworkPlayer {

        public BoltEntity PlayerEntity;
        public BoltConnection Connection;

        public bool IsServer { get { return Connection == null; } }
        public bool IsClient { get { return Connection != null; } }
        public bool IsLocal { get { return PlayerEntity.isControlled; } }

        public BoltEntity SpawnPlayer () {
            if (PlayerEntity == null) {
                PlayerEntity = BoltNetwork.Instantiate (BoltPrefabs.Player, SpawnManager.GetSpawnPoint (), Quaternion.identity);

                if (IsServer)
                    PlayerEntity.TakeControl ();
                else
                    PlayerEntity.AssignControl (Connection);
            }
            return PlayerEntity;
        }
    }
}