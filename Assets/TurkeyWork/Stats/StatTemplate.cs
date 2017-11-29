using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Stats {

    [CreateAssetMenu (menuName = "TurkeyWork/Stats/Stat Template")]
    public class StatTemplate : ScriptableObject {

        public Resource[] Resources;
        public Stat[] Stats;

        
    }

}
