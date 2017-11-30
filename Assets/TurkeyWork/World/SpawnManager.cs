using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.World {

    public class SpawnManager : SingletonBehaviour<SpawnManager> {

        public static bool HasSpawnPoints => Instance.spawnPoints.Count > 0;

        List<SpawnPoint> spawnPoints = new List<SpawnPoint> ();
        int spawnIndex;

        public static void RegisterSpawnPoint (SpawnPoint spawnPoint) {
            Instance.spawnPoints.Add (spawnPoint);
        }

        public static void UnregisterSpawnPoint (SpawnPoint spawnPoint) {
            Instance.spawnPoints.Remove (spawnPoint);
        }

        public static Vector3 GetSpawnPoint (bool allowUnsafe = false) {
            if (allowUnsafe)
                return Instance.spawnPoints[Random.Range (0, Instance.spawnPoints.Count)].transform.position;
            return Instance.spawnPoints[Instance.NextSpawnPoint ()].transform.position;
        }

        int NextSpawnPoint () {
            return spawnIndex++ % spawnPoints.Count;
        }
    }

}