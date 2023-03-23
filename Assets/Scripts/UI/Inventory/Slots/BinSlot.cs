public class BinSlot : InventoryCraftingSlot
{
	#region Methods

	protected override void UpdateInventoryItem()
	{
		if (ItemInSlot == null) return;

		Destroy(ItemInSlot.gameObject);
		ItemInSlot = null;
	}

	public override void ItemPickedUp()
	{
	}

	#endregion
}
