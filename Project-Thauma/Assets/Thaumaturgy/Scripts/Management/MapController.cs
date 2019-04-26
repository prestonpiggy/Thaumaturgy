using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TurkeyWork.World;

public class MapController : MonoBehaviour {

    // Constants
    private const int Background_Canvas = 0, Button_Canvas = 1, Foreground_Canvas = 2;
    // Variables
    [SerializeField]
    private Map worldMap;
    private List<KeyValuePair<Area, Button>> areaPairs;
    private GameObject _areaDescription;
    private float offsetX, offsetY;
    private int _areaNum;
    public GameObject areaDescriptionPrefab;
    public Button[] areaButtons;
    public Transform[] canvasLayers;


    // Use this for initialization
    void Start()
    {
        areaPairs = new List<KeyValuePair<Area, Button>>();
        _areaDescription = Instantiate(areaDescriptionPrefab, canvasLayers[Foreground_Canvas]);
        offsetX = _areaDescription.GetComponent<RectTransform>().sizeDelta.x;
        offsetY = _areaDescription.GetComponent<RectTransform>().sizeDelta.y / 2.25f;
        _areaDescription.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => CloseInfo()); // Get transforms child
        _areaDescription.transform.GetChild(3).GetComponentInChildren<Button>().onClick.AddListener(() => OnAreaSelection()); // Set onClick function with lambda to execute desired function
        _areaDescription.SetActive(false);
        InitMap();
    }

    // Create area / button pairs and set visibility
    void InitMap()
    {
        for (int i = 0; i < worldMap.Areas.Length; i++)
        {
            areaButtons[i].GetComponentInChildren<Text>().text = worldMap.Areas[i].AreaName;
            areaPairs.Add(new KeyValuePair<Area, Button>(worldMap.Areas[i], areaButtons[i]));
            // Set the first area of map available from the start
            if (i == 0)
                areaPairs[i].Key.Available = true;

            // Set button enabled for valid areas
            if (areaPairs[i].Key.Available)
                areaPairs[i].Value.enabled = true;
            Debug.Log(areaPairs[i].Key.Available);
        }
    }

    // Load selected scene
    private void ChangeArea(Area loadArea)
    {
        Debug.Log(loadArea.AreaName + " LOADING");
        WordLevelLayout.LoadLevelWithKey(loadArea.AreaName);
    }

    // Update areas and assosiated components visibility and availability
    private bool UpdateAreas(Area selectedArea)
    {
        if (selectedArea.Available)
        {
            selectedArea.Active = true;

            foreach (Area linkedArea in selectedArea.links)
                linkedArea.Available = true;
            return true;
        }
        return false;
    }

    // Enable and activate linked buttons
    private void SetButtonAvailability(int areaNum)
    {
        foreach (Area area in worldMap.Areas[areaNum].links)
        {
            foreach (KeyValuePair<Area, Button> comparison in areaPairs)
            {
                Debug.Log(comparison.Key + " " + comparison.Key.Available);
                if (area == comparison.Key)
                    comparison.Value.enabled = true;
            }
        }
    }

    // After confirming area selection
    // Update linked areas
    // Set button availability
    // Call area change function
    public void OnAreaSelection()
    {
        if (worldMap.Areas[_areaNum].Available)
        {
            var updated = this.UpdateAreas(worldMap.Areas[_areaNum]);

            if (updated)
                SetButtonAvailability(_areaNum);
            else
                Debug.Log("Error updating areas");
            ChangeArea(worldMap.Areas[_areaNum]);
        }
    }

    // Reset every area except root, for test purposes
    public void ResetAreas()
    {
        for (int i = 1; i < worldMap.Areas.Length; i++)
        {
            var reset = worldMap.Areas[i];
            reset.Active = reset.Available = false;
        }
    }

    // Open info screen and set position according to triggering button
    public void OpenInfo(int areaNum)
    {
        if (areaPairs[areaNum].Key.Available)
        {
            _areaNum = areaNum;
            var pos = areaPairs[areaNum].Value.transform;
            _areaDescription.SetActive(!_areaDescription.activeInHierarchy);
            SetInfo(areaPairs[areaNum].Key);
            _areaDescription.transform.position = new Vector3(pos.position.x + offsetX, pos.position.y - offsetY, 0);
        }
    }

    // Close info screen
    public void CloseInfo()
    {
        _areaDescription.SetActive(!_areaDescription.activeInHierarchy);
    }

    // Set header and body of info field
    private void SetInfo(Area selectedArea)
    {
        _areaDescription.GetComponentsInChildren<Text>()[0].text = selectedArea.AreaName;
        _areaDescription.GetComponentsInChildren<Text>()[1].text = selectedArea.AreaInfo;
    }

}
