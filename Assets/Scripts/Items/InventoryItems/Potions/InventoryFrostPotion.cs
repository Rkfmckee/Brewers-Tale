using UnityEngine;

public class InventoryFrostPotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Frost potion";
	public override string PotionDescription => $"Deals {Damage.Amount} cold damage";
	public override Color PotionColour => Color.blue;
	public override int EnergyCost => 1;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldPotion");

	public Damage Damage => new Damage(2, DamageType.Cold);

	#endregion

	#region Methods

	public override void AffectTarget(Creature target)
	{
		var targetHealth = target.GetComponent<HealthSystem>();
		targetHealth.Damage(Damage);
	}

	#endregion
}