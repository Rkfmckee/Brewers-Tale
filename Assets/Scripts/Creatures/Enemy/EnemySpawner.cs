using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : Singleton<EnemySpawner>
{
	#region Fields

	[SerializeField] private List<Enemy> enemyQueue;
	[SerializeField] private GameObject queuePrefab;

	private EnemySpace spawnSpace;
	private List<GameObject> enemyQueueUI;

	#endregion

	#region Methods

	public void Spawn()
	{
		var spawnSpace = References.EnemySpaces[0];
		if (enemyQueue.Count == 0) return;
		if (spawnSpace.EnemyInSpace != null) return;

		var enemyPrefab = enemyQueue[0].gameObject;
		var enemy = Instantiate(enemyPrefab, spawnSpace.transform.position, enemyPrefab.transform.rotation);

		enemyQueue.RemoveAt(0);
		CreateQueueUI();
	}

	public void CreateQueueUI()
	{
		if (enemyQueueUI != null)
		{
			// Delete previous queue
			foreach (var enemyUI in enemyQueueUI)
			{
				Destroy(enemyUI);
			}
		}

		enemyQueueUI = new List<GameObject>();
		var queue = WorldCanvasManager.Instance.EnemyQueue;

		for (int i = 0; i < enemyQueue.Count; i++)
		{
			var enemy = enemyQueue[i];
			var enemyInQueue = Instantiate(queuePrefab, queue).transform;
			enemyInQueue.Find("Image").GetComponent<Image>().sprite = enemy.QueueIcon;
			enemyInQueue.Find("Name").GetComponent<TextMeshProUGUI>().text = $"{i + 1}. {enemy.EnemyName}";

			enemyQueueUI.Add(enemyInQueue.gameObject);
		}
	}

	#endregion
}
