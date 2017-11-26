using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.AI.State;
using TurkeyWork.Actors;

[CreateAssetMenu(menuName = "AI/States/Chase state")]
public class ChaseState : State
{
    public override void Execute(AIController ai)
    {
        ai.mSpeed = Mathf.Sign(ai.transform.position.x - ai.target.transform.position.x) < 0 ? 2.0f : -2.0f;
        var state = ai.motor.Move(new Vector2(ai.mSpeed * Time.deltaTime, 0));
        Debug.Log(state.CollisionState);
        if (state.CollisionState.HasFlag(CollisionInfo.Left))
            ai.mSpeed = 0f;
        else if (state.CollisionState.HasFlag(CollisionInfo.Right))
            ai.mSpeed = 0f;
    }

    public override void OnStateEnter(AIController ai)
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting MovementState");
    }
}
