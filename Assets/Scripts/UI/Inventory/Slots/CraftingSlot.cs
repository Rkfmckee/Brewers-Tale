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
		return EnergyManager.HaveEnoughEnergy(1, "craft", false);
	}

	protected override void UpdateInventoryItem()
	{
		base.UpdateInventoryItem();

		References.Crafting.ResultSlot.SearchForCraftingRecipe();
	}

	#endregion
}
