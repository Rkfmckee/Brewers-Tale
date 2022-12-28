public class WorldFrostPotion : WorldPotion
{
	#region Fields

	private Damage damage;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		damage = new Damage(2, DamageType.Cold);
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
