using UnityEngine;

[CreateAssetMenu(menuName ="Map/Area")]
public class Area : ScriptableObject {

    [SerializeField]
    private string areaName, areaInfo;
    [SerializeField]
    private bool active, available;
    public Area[] links;

    public bool Active { get { return active; } set { active = value; } }
    public bool Available { get { return available; } set { available = value; } }
    public string AreaName { get { return areaName; } }
    public string AreaInfo { get { return areaInfo; } }
}
