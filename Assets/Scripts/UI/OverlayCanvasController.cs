using TMPro;
using UnityEngine;

public class OverlayCanvasController : MonoBehaviour
{
	#region Fields

	private TextMeshProUGUI currentEnergyText;

	#endregion

	#region Events

	private void Awake()
	{
		References.UI.OverlayCanvas = GetComponent<Canvas>();
		References.UI.OverlayCanvasController = this;

		currentEnergyText = transform.Find("Energy").Find("EnergyLevel").GetComponent<TextMeshProUGUI>();
	}

	#endregion

	#region Methods

	public void SetCurrentEnergyText(int energy)
	{
		currentEnergyText.SetText(energy.ToString());
	}

	#endregion
}
