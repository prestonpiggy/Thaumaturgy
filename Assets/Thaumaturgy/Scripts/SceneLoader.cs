using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Sirenix.OdinInspector;

public class SceneLoader : MonoBehaviour {

    [BoxGroup ("Auto Loading")]
    public bool LoadAtStart;
    [ShowIf ("LoadAtStart"), BoxGroup ("Auto Loading")]
    public float Delay;

    [InfoBox ("NOTE! This can only be used in Non-Networked situations! e.g. Game Start Screens, Main Menus and stuff.")]
    public string SceneToLoad;

    private void Start () {
        if (LoadAtStart)
            Invoke ("LoadDefaultScene", Delay);
    }

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
