public abstract class DamageImmunity : Condition
{
	#region Properties

	public override string Name => $"{ImmuneType} immunity";
	public override string Description => $"{ImmuneType} damage is negated";
	public abstract DamageType ImmuneType { get; }

	#endregion
}