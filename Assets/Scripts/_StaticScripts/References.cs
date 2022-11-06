using System.Collections.Generic;
using UnityEngine;

public static class References
{
	public static InventoryManager InventoryManager;

	public static class Crafting
	{
		public static CraftingRecipeManager RecipeManager;

		public static List<CraftingSlot> Slots = new List<CraftingSlot>();

		public static CraftingResultSlot ResultSlot;
	}

    public static class UI 
	{
		public static Canvas Canvas;
	}
}
