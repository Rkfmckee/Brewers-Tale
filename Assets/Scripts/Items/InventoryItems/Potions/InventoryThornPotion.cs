using UnityEngine;

public class InventoryThornPotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Potion of Thorns";
	public override string PotionDescription => $"{ChanceOfThornWrapped}% chance of Thorn Wrapping target";
	public override Color PotionColour => Color.green;
	public override int EnergyCost => 2;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldPotion");

	public Damage Damage => new Damage(1, DamageType.Physical);
	public int ChanceOfThornWrapped => 100;
	public int ThornWrappedNumberOfTurns => 2;

	#endregion

	#region Methods

	public override void AffectTarget(Creature target)
	{
		if (Random.Range(0, 100) < ChanceOfThornWrapped)
			target.AddCondition(new ThornWrapped(Damage, ThornWrappedNumberOfTurns));
	}

	#endregion
}