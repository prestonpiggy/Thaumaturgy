using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventOnKey : MonoBehaviour {

    public KeyCode Key;
    public UnityEvent OnKey;

    void Update () {
        if (Input.GetKeyDown (Key)) {
            OnKey.Invoke ();
        }
    }
}
