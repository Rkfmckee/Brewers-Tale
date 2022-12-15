using UnityEngine;
using UnityEngine.UI;

public class EndTurn : MonoBehaviour
{
	#region Fields

	private Button endTurnButton;

	#endregion

	#region Events

	private void Awake()
	{
		endTurnButton = GetComponent<Button>();
		endTurnButton.onClick.AddListener(() => EndPlayersTurn());
	}

	#endregion

	#region Methods

	private void EndPlayersTurn()
	{
		if (References.Brewer.CurrentState != BrewerState.Brewing) return;

		References.TurnOrderManager.CurrentTurn = new EnemyTurn();
	}

	#endregion
}
