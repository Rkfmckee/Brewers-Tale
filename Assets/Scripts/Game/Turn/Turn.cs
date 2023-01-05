using TMPro;
using UnityEngine.UI;

public abstract class Turn
{
	#region Properties

	public abstract string TurnText { get; }

	#endregion

	#region Constructors

	public Turn()
	{
		var isPlayerTurn = this is PlayerTurn;
		var endTurnButton = WorldCanvasManager.BookCanvasRight.transform.Find("EndTurn").GetComponent<Button>();
		var endTurnText = isPlayerTurn ? "End turn" : TurnText;
		endTurnButton.interactable = isPlayerTurn;
		endTurnButton.transform.Find("Text").GetComponent<TextMeshProUGUI>().SetText(endTurnText);

		WorldCanvasManager.BookCanvasRight.transform.Find("Energy").gameObject.SetActive(isPlayerTurn);
	}

	#endregion

	#region Events

	public abstract void Update();

	#endregion
}