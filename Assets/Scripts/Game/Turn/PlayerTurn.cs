using TMPro;
using UnityEngine.UI;

public class PlayerTurn : Turn
{
	#region Properties

	public override string TurnText => "Player's turn";

	#endregion

	#region Constructors

	public PlayerTurn() : base()
	{
		var endTurnButton = References.UI.Canvas.transform.Find("EndTurn").GetComponent<Button>();
		endTurnButton.interactable = true;
		endTurnButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "End turn";

		References.TurnOrderManager.CurrentEnergy = 3;
	}

	#endregion

	#region Events

	public override void Update()
	{
	}

	#endregion
}