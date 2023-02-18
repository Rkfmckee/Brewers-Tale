using System.Collections.Generic;
using System.Linq;
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
		endTurnButton.transform.Find("TurnText").GetComponent<TextMeshProUGUI>().SetText(endTurnText);
	}

	#endregion

	#region Events

	public abstract void Update();

	#endregion

	#region Methods

	public abstract void EndTurn();

	protected void EndTemporaryConditions(Creature creature)
	{
		var temporaryConditions = creature.Conditions.OfType<ITemporaryCondition>();
		if (temporaryConditions == null) return;

		var finishedConditions = new List<ITemporaryCondition>();

		foreach (var condition in temporaryConditions)
		{
			condition.NumberOfTurns--;

			if (condition.NumberOfTurns == 0)
			{
				finishedConditions.Add(condition);
			}
		}

		creature.RemoveConditions(finishedConditions);
	}

	#endregion
}