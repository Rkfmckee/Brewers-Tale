using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Turn
{
	#region Fields

	protected Canvas canvas;
	protected TurnOrderManager turnOrderManager;

	#endregion

	#region Properties

	public abstract string TurnText { get; }

	#endregion

	#region Constructors

	public Turn()
	{
		canvas = References.UI.OverlayCanvas;
		turnOrderManager = References.TurnOrderManager;

		var isPlayerTurn = this is PlayerTurn;
		var endTurnButton = canvas.transform.Find("EndTurn").GetComponent<Button>();
		var endTurnText = isPlayerTurn ? "End turn" : TurnText;
		endTurnButton.interactable = isPlayerTurn;
		endTurnButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(endTurnText);

		canvas.transform.Find("Energy").gameObject.SetActive(isPlayerTurn);
	}

	#endregion

	#region Events

	public abstract void Update();

	#endregion
}