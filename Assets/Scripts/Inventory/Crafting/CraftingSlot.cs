public class CraftingSlot : InventoryCraftingSlot
{
	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.Crafting.Slots.Add(this);
	}

	#endregion

	#region Methods

	protected override void UpdateInventoryItem()
	{
		base.UpdateInventoryItem();

		References.Crafting.ResultSlot.SearchForCraftingRecipe();
	}

	#endregion
}
