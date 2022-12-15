public abstract class Turn
{
	#region Fields

	protected TurnOrderManager turnOrderManager;

	#endregion

	#region Properties

	public abstract string TurnText { get; }

	#endregion

	#region Constructors

	public Turn()
	{
		turnOrderManager = References.TurnOrderManager;
	}

	#endregion

	#region Events

	public abstract void Update();

	#endregion
}