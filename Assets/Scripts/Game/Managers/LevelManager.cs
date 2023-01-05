using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Events

	private void Awake()
	{
		References.LevelManager = this;
		TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
	}

	#endregion
}
