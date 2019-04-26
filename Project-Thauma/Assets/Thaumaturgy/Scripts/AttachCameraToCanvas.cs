using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachCameraToCanvas : MonoBehaviour {

	void Start () {
        var canvas = GetComponent<Canvas> ();
        canvas.worldCamera = Camera.main;
	}
	
}
