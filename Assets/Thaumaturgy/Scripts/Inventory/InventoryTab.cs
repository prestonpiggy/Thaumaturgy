using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Inventories;

public class InventoryTab : MonoBehaviour {

    private InventoryController controller;

    private void Awake()
    {
        controller = FindObjectOfType<InventoryController>();
    }

    /// <summary>
    /// When pressing button call controller to change displayed inventory
    /// </summary>
    /// <param name="inventoryNumber"></param>
    public void OnButtonPressed(int inventoryNumber)
    {
        controller.ChangeInventory(inventoryNumber);
    }
}
