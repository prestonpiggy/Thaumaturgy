using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

public class TurkeyEditorStuff {

    [MenuItem ("TurkeyWork/Add Logic Scene")]
	public static void AddLogicScene () {
        UnityEditor.SceneManagement.EditorSceneManager.OpenScene ("Logic", UnityEditor.SceneManagement.OpenSceneMode.Additive);
    }
}

#endif