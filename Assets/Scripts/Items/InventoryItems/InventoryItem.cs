using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
	#region Fields

	private Slot slotInInventory;

	#endregion

	#region Properties

	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	public string ItemName
	{
		get
		{
			if (this is InventoryIngredient) return (this as InventoryIngredient).IngredientName;
			if (this is InventoryPotion) return (this as InventoryPotion).PotionName;
			return "Error";
		}
	}

	public string ItemDescription
	{
		get
		{
			if (this is InventoryIngredient) return (this as InventoryIngredient).IngredientDescription;
			if (this is InventoryPotion) return (this as InventoryPotion).PotionDescription;
			return "Error";
		}
	}
	#endregion
}
