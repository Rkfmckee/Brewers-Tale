using UnityEngine;

public class InventoryIngredient : InventoryItem
{
	#region Fields

	[SerializeField]
	private string ingredientName;
	[SerializeField]
	private string ingredientDescription;

	#endregion

	#region Properties

	public string IngredientName { get => ingredientName; }
	public string IngredientDescription { get => ingredientDescription; }

	#endregion
}
