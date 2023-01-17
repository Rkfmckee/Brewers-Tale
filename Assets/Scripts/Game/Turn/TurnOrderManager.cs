using System.Collections.Generic;
using UnityEngine;

public class TurnOrderManager : Singleton<TurnOrderManager>
{
	#region Fields

	private Turn currentTurn;
	private List<EnemySpace> enemySpaces;

	#endregion

	#region Properties

	public Turn CurrentTurn
	{
		get => currentTurn;
		set
		{
			// If transitioning from another turn, end that one first
			if (currentTurn != null) currentTurn.EndTurn();

			currentTurn = value;
			InventoryManager.Instance.ActiveInventory = currentTurn is PlayerTurn ? InventoryState.Inventory : InventoryState.None;

			InitializeTurn();
		}
	}

	#endregion

	#region Events

	private void Start()
	{
		enemySpaces = References.EnemySpaces;
	}

	private void Update()
	{
		currentTurn.Update();
	}

	#endregion

	#region Methods

	private void InitializeTurn()
	{
		// Some things can only be started from inside a MonoBehaviour
		// Like Coroutines
		// So do that here, rather than in the Turn

		switch (currentTurn)
		{
			case EnemyTurn:
				StartCoroutine((currentTurn as EnemyTurn).StartEnemyTurns());
				break;
		}
	}

	#endregion
}
