using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.AI.State;

namespace StateSystem
{
    [CreateAssetMenu (menuName ="AI/StateMachine")]
    public class StateMachine : ScriptableObject
    {
        private Dictionary<State, Transition[]> states = new Dictionary<State, Transition[]>();
        [SerializeField]
        private StateTransition[] stateTransitions;

        [System.Serializable]
        struct StateTransition
        {
            public State state;
            public Transition[] transition;
        }

        private void OnEnable()
        {
            foreach (var state in stateTransitions)
            {
                states.Add(state.state, state.transition);
            }
        }

        public State Evaluate(State old, AIController aI)
        {
            for(int i = 0; i < states[old].Length; i++)
            {
                var transit = states[old][0].CheckTransitions(old, aI);
                if(old != transit)
                {
                    old.OnStateExit();
                    transit.Execute(aI);
                    return transit;
                }               
            }
            old.Execute(aI);
            return old;
        }
    }
}