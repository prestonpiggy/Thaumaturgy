using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Attack decision")]
public class AttackDecision : Decision
{
    public override bool Decide(AIController aI)
    {
        return EnemyInRange(aI);
    }

    private bool EnemyInRange(AIController aI)
    {
        if (aI.target)
        {
            var distanceToPlayer = aI.transform.position - aI.target.transform.position;
            var x = Mathf.Abs(distanceToPlayer.x);
            var y = Mathf.Abs(distanceToPlayer.y);
            if (x < 1.0f && y < 1.0f)
                return true;
        }
        return false;
    }
}
