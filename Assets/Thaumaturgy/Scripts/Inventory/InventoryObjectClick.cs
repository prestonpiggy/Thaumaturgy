using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TurkeyWork.Inventories;

public class InventoryObjectClick : MonoBehaviour, IPointerClickHandler {

    private Slot slot;
    private InventoryController controller;

    private void Awake()
    {
        slot = GetComponent<Slot>();
        controller = FindObjectOfType<InventoryController>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        slot.OnContentChange(slot.ItemInSlot);
        controller.Initialize();
    }

}
