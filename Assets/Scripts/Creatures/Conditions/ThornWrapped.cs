public class ThornWrapped : TemporaryCondition
{
	#region Properties

	public override string Name => "Thorn Wrapped";
	public override string Description => $"For {NumberOfTurnsText}, when attacked, deal {Damage.Amount} {Damage.Type} damage to attacker";
	public Damage Damage { get; set; }

	#endregion

	#region Constructors

	public ThornWrapped(Damage damage, int numberOfTurns)
	{
		Damage = damage;
		NumberOfTurns = numberOfTurns;
	}

	#endregion
}