using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CraftingRecipeManager : Singleton<CraftingRecipeManager>
{
	#region Properties

	public CraftingRecipe[] Recipes { get; set; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		Recipes = Resources.LoadAll<CraftingRecipe>("CraftingRecipes");
	}

	#endregion

	#region Methods

	public CraftingRecipe FindRecipe(List<InventoryItem> ingredients)
	{
		foreach (var recipe in Recipes)
		{
			if (IngredientsAreTheSame(ingredients, recipe.Ingredients))
			{
				return recipe;
			}
		}

		return null;
	}

	private bool IngredientsAreTheSame(List<InventoryItem> ingredientsA, List<InventoryItem> ingredientsB)
	{
		var lookUp = new Dictionary<string, int>();

		if (ingredientsA == null || ingredientsB == null || ingredientsA.Count != ingredientsB.Count) return false;

		for (int i = 0; i < ingredientsA.Count; i++)
		{
			var count = 0;
			var ingredientName = ingredientsA[i].name;

			if (!lookUp.TryGetValue(ingredientName, out count))
			{
				lookUp.Add(ingredientName, 1);
			}

			lookUp[ingredientName] = count + 1;
		}

		for (int i = 0; i < ingredientsB.Count; i++)
		{
			var count = 0;
			var ingredientName = ingredientsB[i].name;

			if (!lookUp.TryGetValue(ingredientName, out count))
			{
				return false;
			}

			count--;

			if (count <= 0)
				lookUp.Remove(ingredientName);
			else
				lookUp[ingredientName] = count;
		}

		return lookUp.Count == 0;
	}

	#endregion
}