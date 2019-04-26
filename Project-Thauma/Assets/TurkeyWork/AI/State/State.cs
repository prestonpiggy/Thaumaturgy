using UnityEngine;

namespace TurkeyWork.AI.State
{
    public abstract class State : ScriptableObject
    {

        public abstract void OnStateEnter(AIController aI);
        public abstract void OnStateExit();
        public abstract void Execute(AIController aI);
    }
}