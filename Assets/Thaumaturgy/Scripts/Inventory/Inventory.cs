using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Items;

namespace TurkeyWork.Inventories
{
    /// <summary>
    /// Data object for actor inventories
    /// </summary>
    [CreateAssetMenu(menuName ="Inventories/Inventory")]
    public class Inventory : ScriptableObject
    {
        public string Name;
        public Item[] InventoryItems;
        public int inventoryNumber;

        /// <summary>
        /// If inventory contains item, remove it from current inventory
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool RemoveFromInventory(Item item) {
            for(var i = 0; i < InventoryItems.Length; i++)
            {
                if(InventoryItems[i] == item)
                {
                    InventoryItems[i] = null;
                    return true;
                }
            }
            return false;
        }
        
        /// <summary>
        /// Insert item to first available slot
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool AddToInventory(Item item)
        {
            for(var i = 0; i < InventoryItems.Length; i++)
            {
                if(InventoryItems[i] == null) {
                    InventoryItems[i] = item;
                    return true;
                }
            }
            return false;
        }
    }
}