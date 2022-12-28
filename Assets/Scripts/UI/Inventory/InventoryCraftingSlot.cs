public class InventoryCraftingSlot : Slot
{
	#region Methods

	public override void ItemPickedUp()
	{
	}

	public virtual bool CanPlaceItem(InventoryItem item)
	{
		return true;
	}

	#endregion
}