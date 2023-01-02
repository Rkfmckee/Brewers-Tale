public class PlayerTurn : Turn
{
	#region Properties

	public override string TurnText => "Player's turn";

	#endregion

	#region Constructors

	public PlayerTurn() : base()
	{
		EnergyManager.ResetEnergy();
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion
}