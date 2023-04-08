public abstract class DamagingCondition : TemporaryCondition
{
	#region Properties

	public override string Description => $"Deals {Damage.Amount} {Damage.Type} damage for {NumberOfTurnsText}.";
	public Damage Damage { get; set; }

	#endregion

	#region Constructors

	public DamagingCondition(DamageType damageType, int damageAmount, int numberOfTurns)
	{
		Damage = new Damage(damageAmount, damageType);
		NumberOfTurns = numberOfTurns;
	}

	#endregion
}