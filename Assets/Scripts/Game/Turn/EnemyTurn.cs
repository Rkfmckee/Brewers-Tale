using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurn : Turn
{
	#region Fields

	private float startTime;
	private float endTime;

	private List<Enemy> enemies;

	#endregion

	#region Properties

	public override string TurnText => "Enemy's turn";

	#endregion

	#region Constructors

	public EnemyTurn() : base()
	{
		var canvas = References.UI.Canvas;

		var endTurnButton = canvas.transform.Find("EndTurn").GetComponent<Button>();
		endTurnButton.interactable = false;
		endTurnButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = TurnText;

		canvas.transform.Find("Energy").gameObject.SetActive(false);

		enemies = References.Enemies;

		startTime = 1;
		endTime = 0.5f;
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion

	#region Coroutine

	public IEnumerator StartEnemyTurns()
	{
		if (enemies.Count == 0)
		{
			Debug.Log("No enemies to take turns");
			References.TurnOrderManager.CurrentTurn = new PlayerTurn();
			yield break;
		}

		yield return new WaitForSeconds(startTime);

		yield return EnemiesTakeTurns();

		yield return new WaitForSeconds(endTime);

		References.TurnOrderManager.CurrentTurn = new PlayerTurn();
	}

	private IEnumerator EnemiesTakeTurns()
	{
		foreach (var enemy in enemies.OrderByDescending(e => e.CurrentSpace.SpaceNumber))
		{
			enemy.TakeTurn();

			while (enemy.CurrentState != EnemyState.Idle)
			{
				yield return null;
			}
		}
	}

	private IEnumerator WaitUntilEnemiesHaveMoved()
	{
		var finishedMoving = false;

		while (!finishedMoving)
		{
			finishedMoving = true;

			foreach (var enemy in enemies)
			{
				if (enemy.CurrentState == EnemyState.Moving)
					finishedMoving = false;
			}

			if (!finishedMoving) yield return null;
		}
	}

	#endregion
}