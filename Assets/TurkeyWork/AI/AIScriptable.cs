using UnityEngine;


namespace AI_SO
{
    public abstract class AIScriptable : ScriptableObject {

	    public string ActorName { get; protected set; }
        [SerializeField]
        private string actorName;

        public float MovementSpeed { get; protected set; }
        [Range(0, 10.0f)]
        [SerializeField]
        private float movementSpeed;

        public int Damage { get; protected set; }
        [Range(0, 100)]
        [SerializeField]
        private int damage;

        public abstract void Movement();
        public abstract void AttackStateMovement();
        public abstract void AttackPattern();
    }
}