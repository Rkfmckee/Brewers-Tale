public abstract class Condition
{
	#region Properties

	public abstract string Name { get; }
	public abstract string Description { get; }
	public virtual bool IsInherent { get; set; } = false;

	#endregion
}