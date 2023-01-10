public class PlayerTurn : Turn
{
	#region Properties

	public override string TurnText => "Player's turn";

	private Brewer brewer;

	#endregion

	#region Constructors

	public PlayerTurn() : base()
	{
		brewer = References.Brewer;

		EnergyManager.ResetEnergy();
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion

	#region Methods

	public override void EndTurn()
	{
		EndTemporaryConditions(brewer);
	}

	#endregion
}