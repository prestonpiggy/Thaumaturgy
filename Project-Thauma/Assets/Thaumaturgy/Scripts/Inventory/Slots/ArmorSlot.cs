using TurkeyWork.Inventories;
using TurkeyWork.Items;

public class ArmorSlot : Slot{

    
    /// <summary>
    /// Check if the inserted item is valid
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public override bool AllowedInput(Item item)
    {
        if (item.GetType() == typeof(Item) || item == null)
            return true;
        return false;
    }

    public override void UpdateActor(Item item)
    {
        throw new System.NotImplementedException();
    }
}