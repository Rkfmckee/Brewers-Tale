public abstract class DamageResistance : Condition
{
	#region Properties

	public override string Name => $"{ResistedType} resistance";
	public override string Description => $"{ResistedType} damage is halved";
	public abstract DamageType ResistedType { get; }

	#endregion
}