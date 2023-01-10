using UnityEngine;

public class WorldFirePotion : WorldPotion
{
	#region Fields

	private Damage damage;
	private int chanceOfBurning;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		damage = new Damage(2, DamageType.Fire);
		chanceOfBurning = 25;
	}

	#endregion

	#region Methods

	protected override void AffectTarget()
	{
		var targetHealth = Target.GetComponent<HealthSystem>();
		targetHealth.Damage(damage);

		if (Random.Range(0, 100) < chanceOfBurning)
			Target.GetComponent<Creature>().AddCondition(new Burning(damage.Amount, 1));
	}

	#endregion
}
