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
		potionName = "Potion";
		potionDescription = "";
		damage = new List<Damage>();
		condition = conditionEffect;

		foreach (var damageType in damageDict.Keys)
		{
			var damageAmount = damageDict[damageType];
			damage.Add(new Damage(damageAmount, damageType));
			potionDescription += $"{damageAmount} {damageType}, ";
		}

		potionDescription = potionDescription.Trim();
		potionDescription = potionDescription.Trim(',');
	}

	public void AffectTarget(Creature target)
	{
	}

	#endregion
}