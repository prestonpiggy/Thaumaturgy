using System;
using UnityEngine;

namespace TurkeyWork.AI.State
{
    [System.Serializable]
    public class Transition
    {
        public Decision decision;
        public State trueState;
        public State falseState;

        public State CheckTransitions(State current, AIController aI)
        {
            if (decision.Decide(aI))
                return trueState;
            else
                return falseState;
        }
    }
}