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

	protected override void UpdateInventoryItem()
	{
		base.UpdateInventoryItem();

		References.CraftingResultSlot.SearchForCraftingRecipe();
	}

	#endregion
}
