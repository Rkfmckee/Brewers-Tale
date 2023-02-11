public class DamageImmunity : Condition
{
	#region Properties

	public override string Name => $"{ImmuneType} immunity";
	public override string Description => $"{ImmuneType} damage is negated";
	public DamageType ImmuneType { get; }

	#endregion

	#region Constructors

	public DamageImmunity(DamageType type)
	{
		ImmuneType = type;
	}

	#endregion
}