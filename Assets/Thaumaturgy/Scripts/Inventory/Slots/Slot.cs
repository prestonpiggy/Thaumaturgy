using UnityEngine;
using UnityEngine.UI;
using TurkeyWork.Items;
using TurkeyWork.Actors;

namespace TurkeyWork.Inventories
{
    /// <summary>
    /// Base class for slot
    /// </summary>
    public abstract class Slot : MonoBehaviour
    {
        public Item ItemInSlot { get; protected set;}
        [HideInInspector]
        public Sprite ItemSprite { get; protected set; }
        public Inventory ParentInventory, TargetInventory;
        public ActorAttributes actorAttributes;
        private Image image;

        /// <summary>
        /// Set slots item on initialization
        /// </summary>
        /// <param name="item"></param>
        public void Initiliaze(Item item, ActorAttributes attributes, Inventory pInventory, Inventory tInventory)
        {
            image = gameObject.GetComponent<Image>();
            ItemInSlot = item;
            image.color = Random.ColorHSV(1.0f,1.0f,1.0f,1f);
            ParentInventory = pInventory;
            TargetInventory = tInventory;
        }

        /// <summary>
        /// Check is the slot empty
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() { return ItemInSlot == null ? true : false; }

        /// <summary>
        /// Set slot empty
        /// </summary>
        public void SetEmpty() { ItemInSlot = null; }

        /// <summary>
        /// Change the item in slot
        /// </summary>
        /// <param name="item"></param>
        /// <param name="attributes"></param>
        public int OnContentChange(Item item)
        {
            Item temp;
            if(TargetInventory.inventoryNumber == 0)
            {
                temp = TargetInventory.InventoryItems[ParentInventory.inventoryNumber - 1];
                TargetInventory.InventoryItems[ParentInventory.inventoryNumber] = ItemInSlot;
                ParentInventory.RemoveFromInventory(ItemInSlot);
                ParentInventory.AddToInventory(temp);
            }
            else
            {
                TargetInventory.AddToInventory(ItemInSlot);
                ParentInventory.RemoveFromInventory(ItemInSlot);
                SetEmpty();
            }
            return TargetInventory.inventoryNumber == 0 ? ParentInventory.inventoryNumber: TargetInventory.inventoryNumber;
        }

        public abstract bool AllowedInput(Item item);

        public abstract void UpdateActor(Item item);
    }
}