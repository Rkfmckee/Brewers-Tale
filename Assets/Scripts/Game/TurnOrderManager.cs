using System.Collections.Generic;
using UnityEngine;

public class TurnOrderManager : MonoBehaviour
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
			currentTurn = value;
			References.UI.CanvasController.CurrentTurn = currentTurn.TurnText;
			References.InventoryManager.ActiveInventory = value is PlayerTurn ? InventoryState.Inventory : InventoryState.None;
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
}
