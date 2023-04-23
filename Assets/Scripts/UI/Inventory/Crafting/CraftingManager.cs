using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingManager : Singleton<CraftingManager>
{
	#region Fields

	[SerializeField] private GameObject potionPrefab;

	private Dictionary<DamageType, DamageType> oppositeTypes;

	#endregion

	#region Properties

	// Keep this for now, so unique item recipes can be defined
	public CraftingRecipe[] Recipes { get; set; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		oppositeTypes = new Dictionary<DamageType, DamageType>
		{
			{ DamageType.Fire, DamageType.Cold },
			{ DamageType.Cold, DamageType.Fire }
		};
	}

	#endregion

	#region Methods

	public void CheckForValidRecipe()
	{
		RemoveCurrentResult();

		var ingredients = BuildListOfIngredients();
		if (InvalidRecipe(ingredients)) return;

		var potionDamage = GetPotionDamage(ingredients);
		var potionCondition = GetPotionCondition(ingredients);

		// If a valid potion can be created, set it to CraftingResultSlot.ItemInSlot
		if (potionDamage.Count <= 0 && potionCondition == null) return;

		var potion = Instantiate(potionPrefab).GetComponent<InventoryPotion>();
		potion.CreatePotion(potionDamage, potionCondition);
		References.Crafting.ResultSlot.ItemInSlot = potion;
	}

	private void RemoveCurrentResult()
	{
		if (References.Crafting.ResultSlot.ItemInSlot)
		{
			Destroy(References.Crafting.ResultSlot.ItemInstance);
			References.Crafting.ResultSlot.ItemInSlot = null;
		}
	}

	private List<InventoryItem> BuildListOfIngredients()
	{
		var ingredients = new List<InventoryItem>();
		foreach (var craftingSlot in References.Crafting.Slots)
		{
			var ingredient = craftingSlot.ItemInSlot;
			if (ingredient) ingredients.Add(ingredient);
		}

		return ingredients;
	}

	private bool InvalidRecipe(List<InventoryItem> ingredients)
	{
		// If the ingredients don't include either a water bottle or a potion, it's not valid
		var hasWaterBottle = ingredients.Any(i => i is WaterBottle);
		var hasPotion = ingredients.Any(i => i is InventoryPotion);

		// ^ is XOR
		return !(hasWaterBottle ^ hasPotion);
	}

	private Dictionary<DamageType, int> GetPotionDamage(List<InventoryItem> ingredients)
	{
		var potionDamage = new Dictionary<DamageType, int>();

		// Convert ingredients to IItemEffect and combine all damage
		foreach (var ingredient in ingredients)
		{
			// Ignore no effect ingredients, like WaterBottle
			if (ingredient is not IItemEffect) continue;
			var effects = ingredient as IItemEffect;

			foreach (var damage in effects.Damage)
			{
				if (potionDamage.ContainsKey(damage.Type))
					potionDamage[damage.Type] += damage.Amount;
				else
					potionDamage.Add(damage.Type, damage.Amount);
			}
		}

		CheckForOppositeTypes(ref potionDamage);
		return potionDamage;
	}

	private void CheckForOppositeTypes(ref Dictionary<DamageType, int> damageDict)
	{
	restart:
		foreach (var currentType in damageDict.Keys)
		{
			if (!oppositeTypes.ContainsKey(currentType)) continue;
			if (!damageDict.ContainsKey(oppositeTypes[currentType])) continue;

			var currentAmount = damageDict[currentType];
			var oppositeType = oppositeTypes[currentType];
			var oppositeAmount = damageDict[oppositeType];

			var newCurrentAmount = currentAmount - oppositeAmount;
			var newOppositeAmount = oppositeAmount - currentAmount;

			if (newCurrentAmount <= 0)
			{
				damageDict.Remove(currentType);
				damageDict[oppositeType] = newOppositeAmount;
				goto restart;
			}

			if (newOppositeAmount <= 0)
			{
				damageDict.Remove(oppositeType);
				damageDict[currentType] = newCurrentAmount;
				goto restart;
			}
		}
	}

	private Condition GetPotionCondition(List<InventoryItem> ingredients)
	{
		// Decide which condition to use depending on precedence

		return null;
	}

	private bool IngredientsAreTheSame(List<InventoryItem> ingredientsA, List<InventoryItem> ingredientsB)
	{
		// Will only be used alongside Recipes list. If that is removed, this will be too.

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