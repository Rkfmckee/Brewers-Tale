using System.Collections.Generic;
using UnityEngine;

public class CraftingResultSlot : Slot
{	
	#region Events

	protected override void Awake()
	{
		base.Awake();

		References.CraftingResultSlot = this;
	}

	#endregion

	#region Methods

	public void SearchForCraftingRecipe()
	{
		var craftingIngredients = new HashSet<string>();
		var craftingResultName  = null as string;
		var craftingResult      = null as InventoryItem;
		
		foreach (var craftingSlot in References.CraftingSlots)
		{
			if (!craftingSlot.ItemInSlot) continue;

			var itemName = craftingSlot.ItemInSlot.name;
			craftingIngredients.Add(itemName);
		}

		if (!CraftingRecipes.RecipeList.ContainsKey(craftingIngredients))
		{
			ItemInSlot = null;	
			return;
		}
		
		craftingResultName = CraftingRecipes.RecipeList[craftingIngredients];
		craftingResult     = Resources.Load<GameObject>($"Prefabs/Items/{craftingResultName}").GetComponent<InventoryItem>();

		ItemInSlot = craftingResult;
	}

	#endregion
}