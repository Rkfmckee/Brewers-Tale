using TMPro;
using UnityEngine;

public class HealthPopup : MonoBehaviour
{
	#region Fields

	private TextMeshProUGUI popupText;
	private Canvas canvas;

	#endregion

	#region Events

	private void Awake()
	{
		popupText = GetComponent<TextMeshProUGUI>();
	}

	private void Start()
	{
		canvas = References.UI.WorldCanvas;
		transform.SetParent(canvas.transform);
		transform.localScale = Vector3.one;
	}

	#endregion

	#region Methods

	public static void Create(Vector3 position, float amount, bool isDamage)
	{
		var healthPopup = Instantiate(GameAssets.Instance.HealthPopupPrefab, position, Quaternion.identity).GetComponent<HealthPopup>();
		healthPopup.SetText(amount, isDamage);
	}

	public void SetText(float amount, bool isDamage)
	{
		popupText.SetText(amount.ToString());
		popupText.color = isDamage ? Color.red : Color.green;
	}

	#endregion
}
