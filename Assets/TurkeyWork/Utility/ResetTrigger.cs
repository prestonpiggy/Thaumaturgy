using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTrigger : MonoBehaviour {

    private void OnTriggerEnter (Collider other) {
        var player = other.GetComponent<Player> ();

        if (player != null) {
            player.SetPosition (SpawnManager.GetSpawnPoint ());          
        }
    }
}
