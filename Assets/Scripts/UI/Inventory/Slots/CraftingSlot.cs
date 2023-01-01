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
			NotificationManager.Add($"Not enough energy to craft", NotificationType.Error);
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
