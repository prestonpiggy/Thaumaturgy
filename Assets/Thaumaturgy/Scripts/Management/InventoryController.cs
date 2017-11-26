using UnityEngine;
using TurkeyWork.Actors;

namespace TurkeyWork.Inventories
{
    public class InventoryController: MonoBehaviour {

        public GameObject[] equippedItemSlots, inventoryItemSlots, slotPrefabs;
        public Inventory[] actorInventories;
        public ActorAttributes actorAttributes;
        public Transform inventoryTransform;

        public void Start()
        {
            Initialize();
        }

        /// <summary>
        /// Set items to slots and instantiate inventory slots
        /// </summary>
        public void Initialize()
        {
            var i = 0;

            for (i = 0; i < equippedItemSlots.Length; i++)
                equippedItemSlots[i].GetComponent<Slot>().Initiliaze(actorInventories[0].InventoryItems[i], actorAttributes, actorInventories[0], actorInventories[i+1]);

            for (i = 0; i < inventoryItemSlots.Length; i++)
            {
                inventoryItemSlots[i] = Instantiate(slotPrefabs[0], inventoryTransform.position, Quaternion.identity) as GameObject;
                inventoryItemSlots[i].transform.SetParent(inventoryTransform);
                inventoryItemSlots[i].GetComponent<Slot>().Initiliaze(actorInventories[1].InventoryItems[i], actorAttributes, actorInventories[1], actorInventories[0]);
            }
        }

        /// <summary>
        /// Update Inventory UI and display
        /// </summary>
        /// <param name="inventoryNumber"></param>
        public void UpdateInventoryView(int inventoryNumber)
        {
            var i = 0;

            for (i = 0; i < equippedItemSlots.Length; i++)
                equippedItemSlots[i].GetComponent<Slot>().Initiliaze(actorInventories[0].InventoryItems[i], actorAttributes, actorInventories[0], actorInventories[i + 1]);

            for (i = 0; i < inventoryItemSlots.Length; i++)
                inventoryItemSlots[i].GetComponent<Slot>().Initiliaze(actorInventories[inventoryNumber].InventoryItems[i], actorAttributes, actorInventories[inventoryNumber], actorInventories[0]);

            Debug.Log(actorInventories[inventoryNumber].Name);
        }
    }
}