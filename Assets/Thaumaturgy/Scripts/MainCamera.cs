using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

[HideMonoScript]
public class MainCamera : MonoBehaviour {

    Camera mainCamera;
	// Use this for initialization
	void Awake () {
        mainCamera = Camera.main;
        SceneManager.sceneLoaded += OnSceneLoaded;
	}
	
	void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        var cameras = FindObjectsOfType<Camera> ();
        foreach (var cam in cameras)
            if (!cam.Equals (mainCamera))
                cam.gameObject.SetActive (false);
    }

    private void OnDestroy () {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

}
