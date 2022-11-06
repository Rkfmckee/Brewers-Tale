using System.Collections.Generic;
using UnityEngine;

public static class References
{
	public static InventoryManager InventoryManager;

	public static CraftingRecipeManager CraftingRecipeManager;

	public static List<CraftingSlot> CraftingSlots = new List<CraftingSlot>();

	public static CraftingResultSlot CraftingResultSlot;

    public static class UI 
	{
		public static Canvas Canvas;
	}
}
