using System.Collections;
using System.Collections.Generic;
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

	#region Methods

	private void MoveEnemies()
	{
		foreach (var enemy in enemies)
		{
			enemy.MoveToNextSpace();
		}
	}

	#endregion

	#region Coroutine

	public IEnumerator StartEnemyTurns()
	{
		if (enemies.Count == 0)
		{
			Debug.Log("No enemies to take turns");
			yield break;
		}

		yield return new WaitForSeconds(startTime);

		MoveEnemies();
		yield return WaitUntilEnemiesHaveMoved();

		yield return new WaitForSeconds(endTime);

		References.TurnOrderManager.CurrentTurn = new PlayerTurn();
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