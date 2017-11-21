using System;
using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Sighted decision")]
public class LookDecision : Decision
{
    public override bool Decide(AIController aI)
    {
        return EnemySighted(aI);
    }

    private bool EnemySighted(AIController aI)
    {
        foreach (GameObject o in aI.PlayersInInstance)
        {
            var distanceToPlayer = aI.transform.position - o.transform.position;
            var x = Math.Abs(distanceToPlayer.x);
            var y = Math.Abs(distanceToPlayer.y);
            if (x < aI.raycastDistance && y < aI.raycastDistance)
            {
                aI.target = o;
                return true;
            }
        }
        return false;
    }
}
