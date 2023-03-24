
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
	#region Events

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		// Initialize singletons
		TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
		CreatureLocator.Instance.Initialize();
		EnemySpawner.Instance.CreateQueueUI();
	}

	#endregion
}
