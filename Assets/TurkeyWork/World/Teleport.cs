using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using TurkeyWork.Actors;

namespace TurkeyWork.World {

    public class Teleport : MonoBehaviour {

        public enum TeleportType { FixedPosition, Offset, SpawnPoint }

        public TeleportType Mode;
        [HideIf ("Mode", TeleportType.SpawnPoint)]
        public Vector2 Target;

        private void OnTriggerEnter (Collider other) {
            var player = other.GetComponent<PlayerController> ();

            if (player != null) {
                switch (Mode) {
                case TeleportType.FixedPosition:
                    player.SetPosition (player.transform.position = Target);
                    break;
                case TeleportType.Offset:
                    player.SetPosition (player.transform.position += (Vector3) Target);
                    break;
                case TeleportType.SpawnPoint:
                    player.SetPosition (SpawnManager.GetSpawnPoint ());
                    break;
                }
                
            }
        }
    }

}

