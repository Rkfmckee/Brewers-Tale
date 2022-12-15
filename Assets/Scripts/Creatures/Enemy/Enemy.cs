using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Fields

	private float movementTime;
	private EnemySpace currentSpace;

	private List<EnemySpace> enemySpaces;

	#endregion

	#region Properties

	public EnemyState CurrentState { get; set; }
	public EnemySpace CurrentSpace
	{
		get => currentSpace;
		set
		{
			RemoveEnemyFromPreviousSpace();

			currentSpace = value;
			currentSpace.EnemyInSpace = this;
		}
	}


	#endregion

	#region Events

	private void Awake()
	{
		References.Enemies.Add(this);

		movementTime = 1;
		CurrentState = EnemyState.Idle;
	}

	private void Start()
	{
		enemySpaces = References.EnemySpaces;
		CurrentSpace = enemySpaces[0];
	}

	#endregion

	#region Methods

	public void MoveToNextSpace()
	{
		if (CurrentSpace.SpaceNumber == 6) return;

		var nextSpace = enemySpaces[CurrentSpace.SpaceNumber + 1];
		MoveToSpace(nextSpace);
	}

	public void MoveToSpace(EnemySpace space)
	{
		CurrentSpace = space;
		StartCoroutine(MovingToSpace(CurrentSpace));
	}

	private void RemoveEnemyFromPreviousSpace()
	{
		if (currentSpace != null)
			if (GameObject.ReferenceEquals(currentSpace.EnemyInSpace, this))
				currentSpace.EnemyInSpace = null;
	}

	#endregion

	#region Coroutines

	private IEnumerator MovingToSpace(EnemySpace space)
	{
		CurrentState = EnemyState.Moving;

		var startingPosition = enemySpaces[CurrentSpace.SpaceNumber - 1].transform.position;
		var timer = 0f;

		while (Vector3.Distance(transform.position, space.transform.position) > 0)
		{
			transform.position = Vector3.Lerp(startingPosition, space.transform.position, timer / movementTime);
			timer += Time.deltaTime;
			yield return null;
		}

		CurrentState = EnemyState.Idle;
	}

	#endregion
}
