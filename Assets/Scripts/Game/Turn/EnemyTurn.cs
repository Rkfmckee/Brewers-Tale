using UnityEngine;

public class EnemyTurn : Turn
{
	#region Fields

	private float timer;
	private float turnTime;

	#endregion

	#region Properties

	public override string TurnText => "Enemy's turn";

	#endregion

	#region Constructors

	public EnemyTurn() : base()
	{
		timer = 0;
		turnTime = 5;
	}

	#endregion

	#region Events

	public override void Update()
	{
		timer += Time.deltaTime;

		if (timer > turnTime)
		{
			turnOrderManager.CurrentTurn = new PlayerTurn();
		}
	}

	#endregion
}