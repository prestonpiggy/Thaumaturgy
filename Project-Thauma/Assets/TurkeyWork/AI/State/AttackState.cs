using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.AI.State;

[CreateAssetMenu(menuName = "AI/States/Attack state")]
public class AttackState : State
{
    public override void Execute(AIController ai)
    {       
        if(ai.cdTimer > 0.5f)
        {
            Debug.Log ("FUCK");
            //aI.target?.GetComponent<Player>().TakeDamage(aI.damage);
            ai.cdTimer = 0;
        }      
    }

    public override void OnStateEnter(AIController aI)
    {
        aI.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    public override void OnStateExit()
    {
        Debug.Log("Exiting AttackState");
    }
}
