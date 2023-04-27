using System.Linq;
using UnityEngine;

public class CraftingSlot : InventoryCraftingSlot
{
	#region Fields

	[SerializeField][Range(1, 3)] private int slotNumber;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.Crafting.Slots.Add(this);
		References.Crafting.Slots = References.Crafting.Slots.OrderBy(s => s.slotNumber).ToList();
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

		References.Crafting.ResultSlot.CheckForValidRecipe();
	}

	#endregion
}
