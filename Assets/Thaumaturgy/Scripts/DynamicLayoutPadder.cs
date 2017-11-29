using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Sirenix.OdinInspector;

public class DynamicLayoutPadder : MonoBehaviour {

    LayoutGroup layout;

    [Range (0, 1)]
    public float Top;

    private void Start () {
        layout = GetComponent<LayoutGroup> ();
        layout.padding.top = (int) (Screen.height * Top);
    }

    private void OnValidate () {
        if (layout == null)
            layout = GetComponent<LayoutGroup> ();
        layout.padding.top = (int) (Screen.height * Top);
    }
}
