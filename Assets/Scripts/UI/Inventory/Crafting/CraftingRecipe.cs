using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Crafting Recipe", menuName = "Brewers Tale/Crafting Recipe", order = 0)]
public class CraftingRecipe : ScriptableObject
{
	#region Fields

	[SerializeField]
	private List<InventoryItem> ingredients;
	[SerializeField]
	private InventoryItem result;

	#endregion

	#region Properties

	public List<InventoryItem> Ingredients { get => ingredients; }

	public InventoryItem Result { get => result; }

	#endregion
}