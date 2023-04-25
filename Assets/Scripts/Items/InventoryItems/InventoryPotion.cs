using System.Collections.Generic;
using UnityEngine;

public class InventoryPotion : InventoryItem, IItemEffect
{
	#region Fields

	private string potionName;
	private string potionDescription;
	private List<Damage> damage;
	private Condition condition;

	#endregion

	#region Properties

	public override string ItemName => potionName;
	public override string ItemDescription => potionDescription;

	public List<Damage> Damage => damage;
	public Condition Condition => condition;

	public Color PotionColour { get; }
	public int EnergyCost => 1;
	public GameObject WorldPrefab { get; }

	#endregion

	#region Methods

	public void CreatePotion(Dictionary<DamageType, int> damageDict, Condition conditionEffect)
	{
		damage = new List<Damage>();
		condition = conditionEffect;

		foreach (var damageType in damageDict.Keys)
		{
			var damageAmount = damageDict[damageType];
			damage.Add(new Damage(damageAmount, damageType));
		}

		var highestDamageType = GetHighestDamageType(damage);

		potionName = "";
		if (condition != null) potionName += $"{condition.Name} ";
		potionName += "Potion";
		if (highestDamageType.HasValue) potionName += $" of {highestDamageType.GetDescription()}";

		potionDescription = GetEffectDescription();
	}

	public void AffectTarget(Creature target)
	{
	}

	private DamageType? GetHighestDamageType(List<Damage> damageList)
	{
		if (damageList == null) return null;
		if (damageList.Count == 0) return null;

		var highestType = damageList[0].Type;
		var highestAmount = damageList[0].Amount;

		foreach (var damage in damageList)
		{
			if (damage.Amount > highestAmount)
			{
				highestType = damage.Type;
				highestAmount = damage.Amount;
			}
		}

		return highestType;
	}

	#endregion
}