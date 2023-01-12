using UnityEngine;

public class InventoryFirePotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Fire potion";
	public override string PotionDescription => $"Deals {Damage.Amount} fire damage, with {ChanceOfBurning}% chance for Burning";
	public override Color PotionColour => Color.red;
	public override int EnergyCost => 1;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldPotion");

	public Damage Damage => new Damage(2, DamageType.Fire);
	public int ChanceOfBurning => 25;

	#endregion

	#region Methods

	public override void AffectTarget(Creature target)
	{
		var targetHealth = target.GetComponent<HealthSystem>();
		targetHealth.Damage(Damage);

		if (Random.Range(0, 100) < ChanceOfBurning)
			target.AddCondition(new Burning(Damage.Amount, 1));
	}

	#endregion
}