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

    // When clicking on slot change the contents and update Inventory UI via controller
    public void OnPointerClick(PointerEventData eventData)
    {
        var inventoryNum = slot.OnContentChange(slot.ItemInSlot);
        controller.UpdateInventoryView(inventoryNum);
    }
}
