using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : Singleton<CraftingManager>
{
	#region Properties

	// Keep this for now, so unique item recipes can be defined
	public CraftingRecipe[] Recipes { get; set; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();
	}

	#endregion

	#region Methods

	public void CheckForValidRecipe()
	{
		var craftingSlots = References.Crafting.Slots;
		var craftingResultSlot = References.Crafting.ResultSlot;

		// Remove whatever item is currently in CraftingResultSlot
		if (craftingResultSlot.ItemInSlot)
		{
			Destroy(craftingResultSlot.ItemInstance);
			craftingResultSlot.ItemInSlot = null;
		}

		// Get each ingredient in the crafting slots
		var ingredients = new List<InventoryItem>();
		foreach (var craftingSlot in craftingSlots)
		{
			var ingredient = craftingSlot.ItemInSlot;
			if (ingredient) ingredients.Add(ingredient);
		}

		// If the ingredients don't include either a water bottle or a potion, it's not valid
		var hasWaterBottle = ingredients.Any(i => i is WaterBottle);
		var hasPotion = ingredients.Any(i => i is InventoryPotion);

		// Exclusive Or - basically, only continue if either a water bottle or a potion exist,
		// but not if both or neither exist
		if (!(hasWaterBottle ^ hasPotion)) return;
		print("Is valid");

		var potionDamage = new Dictionary<DamageType, int>();
		var potionCondition = null as Condition;

		// Convert ingredients to IItemEffect and combine all damage
		foreach (var ingredient in ingredients)
		{
			// Ignore no effect ingredients, like WaterBottle
			if (ingredient is not IItemEffect) return;

			var damageType = (ingredient as IItemEffect).Damage.Type;
			var damageAmount = (ingredient as IItemEffect).Damage.Amount;

			if (potionDamage.ContainsKey(damageType))
				potionDamage[damageType] += damageAmount;
			else
				potionDamage.Add(damageType, damageAmount);
		}

		// Make sure opposite damage types cancel out

		// Decide which condition to use depending on precedence

		// If a valid potion can be created, set it to CraftingResultSlot.ItemInSlot

		// Otherwise, do nothing.
	}

	// Will only be used alongside Recipes list. If that is removed, this will be too.
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