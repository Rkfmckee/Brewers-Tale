using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyTurn : Turn
{
	#region Fields

	private float startTime;
	private float endTime;
	private List<EnemyState> turnEndStates;

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
		turnEndStates = new List<EnemyState> { EnemyState.Idle, EnemyState.Dead };

		enemySpawner.Spawn();
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion

	#region Coroutine

	public override void EndTurn()
	{
		foreach (var enemy in enemies)
		{
			EndTemporaryConditions(enemy);
		}
	}

	public IEnumerator StartEnemyTurns()
	{
		if (enemies.Count == 0)
		{
			Debug.Log("No enemies to take turns");
			TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
			yield break;
		}

		yield return new WaitForSeconds(startTime);

		yield return EnemiesTakeTurns();

		yield return new WaitForSeconds(endTime);

		TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
	}

	private IEnumerator EnemiesTakeTurns()
	{
		foreach (var enemy in enemies.OrderByDescending(e => e.CurrentSpace.SpaceNumber))
		{
			enemy.TakeTurn();

			while (!turnEndStates.Contains(enemy.CurrentState))
			{
				yield return null;
			}
		}
	}

	#endregion
}