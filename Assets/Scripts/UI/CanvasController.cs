using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
	#region Fields

	private TextMeshProUGUI currentEnergyText;

	#endregion

	#region Events

	private void Awake()
	{
		References.UI.Canvas = GetComponent<Canvas>();
		References.UI.CanvasController = this;

		currentEnergyText = transform.Find("Energy").Find("EnergyLevel").GetComponent<TextMeshProUGUI>();
	}

	#endregion

	#region Methods

	public void SetCurrentEnergyText(int energy)
	{
		currentEnergyText.text = energy.ToString();
	}

	#endregion
}
