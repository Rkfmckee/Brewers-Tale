public class WorldFirePotion : WorldPotion
{
	#region Fields

	private Damage damage;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		damage = new Damage(2, DamageType.Fire);
	}

	#endregion

	#region Methods

	protected override void AffectTarget()
	{
		var targetHealth = Target.GetComponent<HealthSystem>();
		targetHealth.Damage(damage);
	}

	#endregion
}
