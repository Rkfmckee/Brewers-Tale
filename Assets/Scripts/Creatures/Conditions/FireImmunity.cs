public class FireImmunity : DamageImmunity
{
	#region Properties

	public override string Name => "Fire immunity";
	public override string Description => "Fire damage is negated";
	public override DamageType ImmuneType => DamageType.Fire;

	#endregion
}