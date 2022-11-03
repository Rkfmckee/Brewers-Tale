public class CraftingSlot : InventoryCraftingSlot
{
	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.CraftingSlots.Add(this);
	}

	#endregion

	#region Methods

	protected override void UpdateInventorySlot()
	{
		base.UpdateInventorySlot();

		References.CraftingResultSlot.SearchForCraftingRecipe();
	}

	#endregion
}
