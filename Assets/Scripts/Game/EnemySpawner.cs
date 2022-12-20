using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	#region Fields

	private EnemySpace spawnSpace;

	private GameObject enemyPrefab;

	#endregion

	#region Events

	private void Start()
	{
		enemyPrefab = Resources.Load<GameObject>("Prefabs/Characters/Enemies/Goblin");

		spawnSpace = References.EnemySpaces[0];
	}

	private void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			if (spawnSpace.EnemyInSpace != null) return;

			var enemy = Instantiate(enemyPrefab, spawnSpace.transform.position, enemyPrefab.transform.rotation);
		}
	}

	#endregion
}
