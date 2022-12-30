using System.Collections.Generic;
using UnityEngine;

public class TurnOrderManager : MonoBehaviour
{
	#region Fields

	private Turn currentTurn;
	private int currentEnergy;
	private List<EnemySpace> enemySpaces;

	#endregion

	#region Properties

	public Turn CurrentTurn
	{
		get => currentTurn;
		set
		{
			currentTurn = value;
			References.InventoryManager.ActiveInventory = currentTurn is PlayerTurn ? InventoryState.Inventory : InventoryState.None;

			InitializeTurn();
		}
	}

	public int CurrentEnergy
	{
		get => currentEnergy;
		set
		{
			currentEnergy = value;
			References.UI.OverlayCanvasController.SetCurrentEnergyText(currentEnergy);
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		References.TurnOrderManager = this;
	}

	private void Start()
	{
		enemySpaces = References.EnemySpaces;
		CurrentTurn = new PlayerTurn();
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
