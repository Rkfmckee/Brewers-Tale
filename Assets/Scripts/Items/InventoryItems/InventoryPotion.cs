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

	public virtual List<Damage> Damage => damage;
	public virtual Condition Condition => condition;

	public virtual Color PotionColour { get; }
	public virtual int EnergyCost { get; }
	public virtual GameObject WorldPrefab { get; }

	#endregion

	#region Methods

	public void CreatePotion(string name, string description, List<Damage> damage, Condition condition)
	{
		potionName = name;
		potionDescription = description;
		this.damage = damage;
		this.condition = condition;
	}

	public void AffectTarget(Creature target)
	{

	}

	#endregion
}