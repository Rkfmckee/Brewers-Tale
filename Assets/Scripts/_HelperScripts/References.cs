using System.Collections.Generic;
using UnityEngine;

public static class References
{
	public static Brewer Brewer;
	public static InventoryManager InventoryManager;
	public static TurnOrderManager TurnOrderManager;
	public static List<Enemy> Enemies = new List<Enemy>();
	public static List<EnemySpace> EnemySpaces = new List<EnemySpace>();
	public static EnemySpawner EnemySpawner;

	public static class Crafting
	{
		public static CraftingRecipeManager RecipeManager;
		public static List<CraftingSlot> Slots = new List<CraftingSlot>();
		public static CraftingResultSlot ResultSlot;
	}

	public static class UI
	{
		public static Canvas OverlayCanvas;
		public static Canvas WorldCanvas;
		public static OverlayCanvasController OverlayCanvasController;
		public static WorldCanvasController WorldCanvasController;
	}
}
