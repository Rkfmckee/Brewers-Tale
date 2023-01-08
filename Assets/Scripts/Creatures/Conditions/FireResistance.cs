public class FireResistance : DamageResistance
{
	#region Properties

	public override string Name => "Fire resistance";
	public override string Description => "Fire damage is halved";
	public override DamageType ResistedType => DamageType.Fire;

	#endregion
}