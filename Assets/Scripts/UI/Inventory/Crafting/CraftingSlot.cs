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

	public override bool CanPlaceItem(InventoryItem item)
	{
		if (References.TurnOrderManager.CurrentEnergy < 1)
		{
			print("Not enough energy to craft");
			return false;
		}

		return true;
	}

	protected override void UpdateInventoryItem()
	{
		base.UpdateInventoryItem();

		References.Crafting.ResultSlot.SearchForCraftingRecipe();
	}

	#endregion
}
