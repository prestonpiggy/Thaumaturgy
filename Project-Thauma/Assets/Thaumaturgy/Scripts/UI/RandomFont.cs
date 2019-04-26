using UnityEngine;
using UnityEngine.UI;

public class RandomFont : MonoBehaviour {

    public Font sceneFont;

    private void Awake()
    {
        var sceneTexts = FindObjectsOfType<Text>();

        foreach (Text t in sceneTexts)
        {
            t.font = sceneFont;
            t.fontSize = 24;
            t.fontStyle = FontStyle.Bold;
        }
            
    }
}
