using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TurkeyWork.World {

    public class LevelExit : MonoBehaviour {
#if UNITY_EDITOR
        [InfoBox ("A Collider set as trigger is required!", "HasInvalidCollier")]
        Collider col;
#endif

        public bool RequireAllPlayers = true;
        public string LevelLoadKey;

        private void OnTriggerEnter2D (Collider2D other) {
            if (AllPlayersInRange ()) {
                LoadScene ();
            }
        }

        void LoadScene () {
            print ("Perhaps loading a Scene");
            WordLevelLayout.LoadLevelWithKey (LevelLoadKey);
        }

        void SavePlayer () {
            print ("Skipping trigering of player data saving");
        }

        bool AllPlayersInRange () {
            print ("Skipping players in range check");
            return true;
        }

#if UNITY_EDITOR
        bool HasInvalidCollier () {
            return col == null || !col.isTrigger;
        }
#endif

    }

}