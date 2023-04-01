using UnityEngine;

public class InventorySharpPotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Potion of Sharpness";
	public override string PotionDescription => $"Deals {Damage.Amount} {Damage.Type} damage";
	public override Color PotionColour => Color.grey;
	public override int EnergyCost => 1;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldPotion");

	public Damage Damage => new Damage(1, DamageType.Physical);

	#endregion

	#region Methods

	public override void AffectTarget(Creature target)
	{
		var targetHealth = target.GetComponent<HealthSystem>();
		targetHealth.Damage(Damage);
	}

	#endregion
}