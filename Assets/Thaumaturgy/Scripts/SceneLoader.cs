using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class SceneLoader : MonoBehaviour {

    [InfoBox ("NOTE! This can only be used in Non-Networked situations! e.g. Game Start Screens, Main Menus and stuff.")]
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
