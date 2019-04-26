using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TurkeyWork.Actors;
namespace TurkeyWork.World {

    public class Ladder : MonoBehaviour {

        private void OnTriggerEnter2D (Collider2D other) {
            var player = other.GetComponent<PlayerController> ();

            if (player != null)
                player.OnLadder = true;
        }

        private void OnTriggerExit2D (Collider2D other) {
            var player = other.GetComponent<PlayerController> ();

            if (player != null)
                player.OnLadder = false;
        }

    }

}