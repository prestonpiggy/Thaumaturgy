﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TurkeyWork.Inventories;
using TurkeyWork.Items;

public class InventorySlot : Slot
{
    public override bool AllowedInput(Item item)
    {
        return item != null;
    }

    public override void UpdateActor(Item item)
    {
    }
}