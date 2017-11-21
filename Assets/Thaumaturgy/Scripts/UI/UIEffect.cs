using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class UIEffect : MonoBehaviour {

    [SerializeField] protected UnityEvent OnEffectPlayed;

    public float StartDelay;

    public abstract void PlayEffect ();
}
