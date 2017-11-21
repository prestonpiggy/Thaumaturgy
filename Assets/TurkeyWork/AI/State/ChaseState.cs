using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.AI.State;

[CreateAssetMenu(menuName = "AI/States/Chase state")]
public class ChaseState : State
{
    public override void Execute(AIController aI)
    {
        var direction = Mathf.Sign(aI.transform.position.x - aI.target.transform.position.x);
        aI.GetComponent<Rigidbody2D>().velocity = new Vector2(direction < 0 ? 2.0f : -2.0f, 0);
    }

    public override void OnStateEnter(AIController aI)
    {
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting MovementState");
    }
}
