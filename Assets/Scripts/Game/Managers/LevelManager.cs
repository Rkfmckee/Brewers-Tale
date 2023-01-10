using UnityEngine;

public class LevelManager : MonoBehaviour
{
	#region Events

	private void Awake()
	{
		References.LevelManager = this;
	}

	private void Start()
	{
		TurnOrderManager.Instance.CurrentTurn = new PlayerTurn();
	}

	#endregion
}
