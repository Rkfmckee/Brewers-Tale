using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurn : Turn
{
	#region Fields

	private float startTime;
	private float endTime;

	private List<Enemy> enemies;
	private EnemySpawner enemySpawner;

	#endregion

	#region Properties

	public override string TurnText => "Enemy's turn";

	#endregion

	#region Constructors

	public EnemyTurn() : base()
	{
		enemies = References.Enemies;
		enemySpawner = References.EnemySpawner;

		startTime = 1;
		endTime = 0.5f;

		enemySpawner.Spawn();
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

	#endregion
}