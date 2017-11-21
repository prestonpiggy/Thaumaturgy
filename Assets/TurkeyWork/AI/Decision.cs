using UnityEngine;

namespace TurkeyWork.AI.Dec
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AIController aI);
    }
}