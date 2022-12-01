using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
	#region Fields

	private string currentTurn;

	private TextMeshProUGUI currentTurnText;

	#endregion

	#region Properties

	public string CurrentTurn
	{
		set
		{
			currentTurn = value;
			currentTurnText.text = value;
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		References.UI.Canvas = GetComponent<Canvas>();
		References.UI.CanvasController = this;
		currentTurnText = transform.Find("CurrentTurn").GetComponent<TextMeshProUGUI>();
	}

	#endregion
}
