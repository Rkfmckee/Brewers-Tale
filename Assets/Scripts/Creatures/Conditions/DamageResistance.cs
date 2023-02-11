public class DamageResistance : Condition
{
	#region Properties

	public override string Name => $"{ResistedType} resistance";
	public override string Description => $"{ResistedType} damage is halved";
	public DamageType ResistedType { get; }

	#endregion

	#region Constructors

	public DamageResistance(DamageType type)
	{
		ResistedType = type;
	}

	#endregion
}