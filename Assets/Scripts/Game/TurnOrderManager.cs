using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnOrderManager : MonoBehaviour
{
	#region Fields

	private Turn currentTurn;
	private List<EnemySpace> enemySpaces;

	private CanvasController canvas;

	#endregion

	#region Properties

	public Turn CurrentTurn
	{
		get => currentTurn;
		set
		{
			currentTurn = value;
			canvas.CurrentTurn = currentTurn.TurnText;
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
		canvas = References.UI.CanvasController;
		enemySpaces = References.EnemySpaces.OrderBy(e => e.SpaceNumber).ToList();
		CurrentTurn = new PlayerTurn();
	}

	#endregion
}
