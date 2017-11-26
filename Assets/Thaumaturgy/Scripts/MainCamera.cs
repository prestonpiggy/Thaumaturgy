using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        var cameras = FindObjectsOfType<Camera> ();
        foreach (var cam in cameras)
            if (cam.Equals (this))
                cam.gameObject.SetActive (false);
    }

    private void OnDestroy () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
