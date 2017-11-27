using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TurkeyWork.Abilities {

    public interface IAbilityUser {

        MonoBehaviour Behaviour { get; }

        void OnAbilityCanceled ();

        void OnAbilityInterrupted ();

    }

}