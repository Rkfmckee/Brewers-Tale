using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private List<GameObject> enemyQueue;

	private EnemySpace spawnSpace;

	#endregion

	#region Events

	private void Awake()
	{
		References.EnemySpawner = this;
	}

	private void Start()
	{
		spawnSpace = References.EnemySpaces[0];
	}

	#endregion

	#region Methods

	public void Spawn()
	{
		if (enemyQueue.Count == 0) return;
		if (spawnSpace.EnemyInSpace != null) return;

		var enemyPrefab = enemyQueue[0];
		var enemy = Instantiate(enemyPrefab, spawnSpace.transform.position, enemyPrefab.transform.rotation);

		enemyQueue.RemoveAt(0);
	}

	#endregion
}
