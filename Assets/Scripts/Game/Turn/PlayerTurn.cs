public class PlayerTurn : Turn
{
	#region Properties

	public override string TurnText => "Player's turn";

	#endregion

	#region Constructors

	public PlayerTurn() : base()
	{
		References.TurnOrderManager.CurrentEnergy = 3;
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion
}