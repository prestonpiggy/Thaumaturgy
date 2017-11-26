using UnityEngine;

[CreateAssetMenu(menuName ="Map/Map")]
public class Map : ScriptableObject {

    public string mapName;
    [SerializeField]
    private Area[] areas;

    public Area[] Areas => areas;
}
