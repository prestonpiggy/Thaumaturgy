using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.AI.State;

[CreateAssetMenu(menuName = "AI/States/Chase state")]
public class ChaseState : State
{
    public override void Execute(AIController ai)
    {
        var direction = Mathf.Sign(ai.transform.position.x - ai.target.transform.position.x);
        ai.transform.Translate (new Vector2 (direction < 0 ? 2.0f : -2.0f, 0) * Time.deltaTime);
    }

    public override void OnStateEnter(AIController ai)
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting MovementState");
    }
}
