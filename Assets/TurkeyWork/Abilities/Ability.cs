using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    [CreateAssetMenu (menuName = "Game/Ability")]
    public class Ability : ScriptableObject {

        public float Cooldown;
        public int Cost;

        public AbilityState[] State;

        public GameObject EffectPrefab;
        public Projectile ProjectilePrefab;

        public void Use (Player player) {
            Debug.Log ($"{player.name} used Ability {name}");

            if (EffectPrefab != null)
                Instantiate (EffectPrefab, player.transform.position, player.transform.rotation);
            if (ProjectilePrefab != null)
                ProjectilePrefab.Create (player, player.transform.position, player.transform.rotation);
        }
    }

}