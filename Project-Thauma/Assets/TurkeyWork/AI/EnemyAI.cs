using UnityEngine;
using AI_SO;

[CreateAssetMenu(menuName = "AI/EnemyAI")]
public class EnemyAI : AIScriptable {

    private void Awake()
    {
        //ActorName = HAE TÄHÄN MIKÄLI TARVITAAN;
        //MovementSpeed = HAE TÄHÄN ACTORILTA;
        //Damage = HAE TÄHÄN ACTORILTA;
    }

    public override void Movement()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackStateMovement()
    {
        throw new System.NotImplementedException();
    }

    public override void AttackPattern()
    {
        throw new System.NotImplementedException();
    }
}
