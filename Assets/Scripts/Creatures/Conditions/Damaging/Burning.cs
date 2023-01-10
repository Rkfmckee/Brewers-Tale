public class Burning : DamagingCondition
{
	#region Properties

	public override string Name => "Burning";

	#endregion

	#region Constructors

	public Burning(int damageAmount, int numberOfTurns)
			: base(DamageType.Fire, damageAmount, numberOfTurns)
	{
		Damage = new Damage(damageAmount, DamageType.Fire);
		NumberOfTurns = numberOfTurns;
	}

	#endregion
}