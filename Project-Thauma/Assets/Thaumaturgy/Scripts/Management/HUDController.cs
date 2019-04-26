using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Actors;

public class HUDController : MonoBehaviour {

    [RuntimeInitializeOnLoadMethod]
    public static void Initialize()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HUD", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}
