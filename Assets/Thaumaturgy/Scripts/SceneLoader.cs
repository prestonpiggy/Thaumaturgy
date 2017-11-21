using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    public string SceneToLoad;

    public void LoadDefaultScene () {
        if (!string.IsNullOrEmpty (SceneToLoad))
            SceneManager.LoadScene (SceneToLoad);
    }

	public void LoadScene (string name) {
        if (!string.IsNullOrEmpty (name))
            SceneManager.LoadScene (SceneToLoad);
    }

    public void LoadScene (int index) {
        SceneManager.LoadScene (index);
    }

}
