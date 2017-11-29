using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.World {

    [BoltGlobalBehaviour]
    public class SpawnManager : Bolt.GlobalEventListener {

        static SpawnManager instance;

        public static bool HasSpawnPoints => instance.spawnPoints.Count > 0;

        List<SpawnPoint> spawnPoints = new List<SpawnPoint> ();
        int spawnIndex;

        private void Awake () {
            if (instance == null) {
                instance = this;
            }
        }

        public static void RegisterSpawnPoint (SpawnPoint spawnPoint) {
            instance.spawnPoints.Add (spawnPoint);
        }

        public static void UnregisterSpawnPoint (SpawnPoint spawnPoint) {
            instance.spawnPoints.Remove (spawnPoint);
        }

        public static Vector3 GetSpawnPoint (bool allowUnsafe = false) {
            if (allowUnsafe)
                return instance.spawnPoints[Random.Range (0, instance.spawnPoints.Count)].transform.position;
            return instance.spawnPoints[instance.NextSpawnPoint ()].transform.position;
        }

        int NextSpawnPoint () {
            return spawnIndex++ % spawnPoints.Count;
        }
    }

}