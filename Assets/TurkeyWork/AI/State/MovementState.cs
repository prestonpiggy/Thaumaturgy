using UnityEngine;
using TurkeyWork.AI.State;

[CreateAssetMenu(menuName = "AI/States/Movement state")]
public class MovementState : State
{
    public override void Execute(AIController ai)
    {
        if (!ai.LGroundRay)
            ai.mSpeed = 2.0f;
        else if (!ai.RGroundRay)
            ai.mSpeed = -2.0f;

        ai.transform.Translate (new Vector2 (ai.mSpeed * Time.deltaTime, 0));
    }

    public override void OnStateEnter(AIController aI)
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting MovementState");
    }
}
