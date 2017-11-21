using UnityEngine;
using TurkeyWork.AI.State;
using TurkeyWork.Actors;

[CreateAssetMenu(menuName = "AI/States/Movement state")]
public class MovementState : State
{
    public override void Execute(AIController ai)
    {/*
        if (!ai.LGroundRay)
            ai.mSpeed = 2.0f;
        else if (!ai.RGroundRay)
            ai.mSpeed = -2.0f;
            */
        //ai.transform.Translate (new Vector2 (ai.mSpeed * Time.deltaTime, 0));
        var state = ai.motor.Move (new Vector2 (ai.mSpeed * Time.deltaTime, 0));
        Debug.Log (state.CollisionState);
        if (state.CollisionState.HasFlag (CollisionInfo.Left))
            ai.mSpeed = 2.0f;
        else if (state.CollisionState.HasFlag (CollisionInfo.Right))
            ai.mSpeed = -2.0f;
    }

    public override void OnStateEnter(AIController aI)
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting MovementState");
    }
}
