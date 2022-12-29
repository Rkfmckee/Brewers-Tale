using System.Collections.Generic;

public class CraftingResultSlot : Slot
{
	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.Crafting.ResultSlot = this;
	}

	#endregion

	#region Methods

	public void SearchForCraftingRecipe()
	{
		var craftingRecipeManager = References.Crafting.RecipeManager;
		var craftingIngredients = new List<InventoryItem>();
		var craftingRecipe = null as CraftingRecipe;

		foreach (var craftingSlot in References.Crafting.Slots)
		{
			if (!craftingSlot.ItemInSlot) continue;

			var ingredient = craftingSlot.ItemInSlot.GetComponent<InventoryItem>();
			if (ingredient) craftingIngredients.Add(ingredient);
		}

		if (craftingIngredients.Count == 0) return;

		craftingRecipe = craftingRecipeManager.FindRecipe(craftingIngredients);
		if (!craftingRecipe)
		{
			Destroy(itemInstance);
			ItemInSlot = null;

			return;
		}

		ItemInSlot = craftingRecipe.Result;
	}

	public override void ItemPickedUp()
	{
		// Remove the crafting ingredients
		foreach (var craftingSlot in References.Crafting.Slots)
		{
			if (!craftingSlot.ItemInSlot) continue;

			Destroy(craftingSlot.ItemInSlot.gameObject);
			craftingSlot.ItemInSlot = null;
		}

		References.TurnOrderManager.CurrentEnergy -= 1;
	}

	#endregion
}