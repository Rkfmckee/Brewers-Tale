using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CraftingResultSlot : Slot
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

		References.Crafting.ResultSlot = this;

		oppositeTypes = new Dictionary<DamageType, DamageType>
		{
			{ DamageType.Fire, DamageType.Cold },
			{ DamageType.Cold, DamageType.Fire }
		};
	}

	#endregion

	#region Methods

	public override void ItemPickedUp()
	{
		// Remove the crafting ingredients
		foreach (var craftingSlot in References.Crafting.Slots)
		{
			if (!craftingSlot.ItemInSlot) continue;

			Destroy(craftingSlot.ItemInSlot.gameObject);
			craftingSlot.ItemInSlot = null;
		}

		EnergyManager.UseEnergy(1);
	}

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
		ItemInSlot = potion;
	}

	private void RemoveCurrentResult()
	{
		if (ItemInSlot)
		{
			Destroy(ItemInstance);
			ItemInSlot = null;
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
		if (ingredients.Count == 0) return true;
		if (!ingredients.Any(i => i is IItemEffect)) return true;

		// If the ingredients don't include either a water bottle or a potion, it's not valid
		// ^ is XOR
		if (!(ingredients.Any(i => i is WaterBottle) ^ ingredients.Any(i => i is InventoryPotion))) return true;

		// If the only ingredient is a potion
		if (ingredients.Count == 1 && ingredients.Count(i => i is InventoryPotion) == 1) return true;

		return false;
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

			if (effects.Damage == null || effects.Damage.Count == 0) continue;

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
		// The condition first encountered is preferable, so player has control of which to use
		var condition = null as Condition;

		foreach (var ingredient in ingredients)
		{
			// Ignore no effect ingredients, like WaterBottle
			if (ingredient is not IItemEffect) continue;
			var effects = ingredient as IItemEffect;

			if (effects.Condition == null) continue;
			if (condition == null) condition = effects.Condition;
		}

		return condition;
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