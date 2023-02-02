using System.Collections.Generic;

public static class References
{
	public static List<Creature> Creatures = new List<Creature>();
	public static Brewer Brewer;
	public static List<Enemy> Enemies = new List<Enemy>();
	public static List<EnemySpace> EnemySpaces = new List<EnemySpace>();

	public static class Inventory
	{
		public static List<InventorySlot> Slots = new List<InventorySlot>();
	}

	public static class Crafting
	{
		public static List<CraftingSlot> Slots = new List<CraftingSlot>();
		public static CraftingResultSlot ResultSlot;
	}
}
