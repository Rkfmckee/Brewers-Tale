public abstract class DamagingCondition : Condition, ITemporaryCondition
{
	#region Properties

	public override string Description => $"Deals {Damage.Amount} {Damage.Type} damage for {NumberOfTurns} {TurnOrTurns()}.";
	public Damage Damage { get; set; }
	public int NumberOfTurns { get; set; }

	#endregion

	#region Constructors

	public DamagingCondition(DamageType damageType, int damageAmount, int numberOfTurns)
	{
		Damage = new Damage(damageAmount, damageType);
		NumberOfTurns = numberOfTurns;
	}

	#endregion

	#region Methods

	private string TurnOrTurns()
	{
		if (NumberOfTurns == 1) return "turn";
		return "turns";
	}

	#endregion
}