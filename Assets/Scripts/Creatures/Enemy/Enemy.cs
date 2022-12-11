using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	#region Fields

	private float movementTime;

	private List<EnemySpace> enemySpaces;

	#endregion

	#region Properties

	public EnemySpace CurrentSpace { get; set; }
	public EnemyAction CurrentAction { get; set; }

	#endregion

	#region Events

	private void Awake()
	{
		References.Enemies.Add(this);

		movementTime = 1;
		CurrentAction = EnemyAction.None;
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

		CurrentSpace = enemySpaces[CurrentSpace.SpaceNumber + 1];
		MoveToSpace(CurrentSpace);
	}

	public void MoveToSpace(EnemySpace space)
	{
		StartCoroutine(MovingToSpace(CurrentSpace));
	}

	#endregion

	#region Coroutines

	private IEnumerator MovingToSpace(EnemySpace space)
	{
		CurrentAction = EnemyAction.Moving;

		var startingPosition = enemySpaces[CurrentSpace.SpaceNumber - 1].transform.position;
		var timer = 0f;

		while (Vector3.Distance(transform.position, space.transform.position) > 0)
		{
			transform.position = Vector3.Lerp(startingPosition, space.transform.position, timer / movementTime);
			timer += Time.deltaTime;
			yield return null;
		}

		CurrentAction = EnemyAction.None;
	}

	#endregion
}
