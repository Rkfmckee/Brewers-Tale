
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
	#region Events

	private void Start()
	{
		DontDestroyOnLoad(gameObject);

		TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
	}

	#endregion
}
