using UnityEngine;

[CreateAssetMenu(menuName ="AI/Decisions/Roam decision")]
public class RoamDecision : Decision
{
    public override bool Decide(AIController aI)
    {
        return TargetOnSight(aI);
    }

    private bool TargetOnSight(AIController aI)
    {
        if(aI.target && aI.target.transform.position.x > 10.0f || aI.target && aI.target.transform.position.y > 10.0f)
            return true;
        return false;
    }
}
