public class InventorySlot : InventoryCraftingSlot
{
	#region Properties

	public int SlotNumber { get; set; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.Inventory.Slots.Add(this);
	}

	#endregion
}
