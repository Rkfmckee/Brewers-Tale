using System.Collections;
using TMPro;
using UnityEngine;

public class HealthPopup : MonoBehaviour
{
	#region Constants

	private const float SIDEWAYS_MOVEMENT_PER_FRAME = 0.05f;
	private const float SIDEWAYS_MOVEMENT_LIMIT = 5f;
	private const float UPWARDS_MOVEMENT_PER_FRAME = 0.1f;

	#endregion

	#region Fields

	private float fadeInSeconds;
	private float fadeOutSeconds;
	private float direction;

	private TextMeshProUGUI popupText;
	private Canvas canvas;
	public Vector3 healthBarPosition;

	#endregion

	#region Events

	private void Awake()
	{
		popupText = GetComponent<TextMeshProUGUI>();

		fadeInSeconds = 0.5f;
		fadeOutSeconds = 1;
		direction = SIDEWAYS_MOVEMENT_PER_FRAME;
	}

	private void Start()
	{
		canvas = WorldCanvasManager.Canvas;
		transform.SetParent(canvas.transform);
		transform.localScale = Vector3.one;
	}

	private void Update()
	{
		var xPositionDifference = Mathf.Abs(transform.localPosition.x - healthBarPosition.x);
		if (xPositionDifference >= SIDEWAYS_MOVEMENT_LIMIT) direction *= -1;

		transform.localPosition += new Vector3(direction, UPWARDS_MOVEMENT_PER_FRAME, 0);
	}

	#endregion

	#region Methods

	public void Initialize((string, NotificationType, HealthBar) healthPopupInfo)
	{
		var text = healthPopupInfo.Item1;
		var type = healthPopupInfo.Item2;
		var healthBar = healthPopupInfo.Item3;

		var colour = type == NotificationType.Success ? Color.green : Color.red;
		colour.a = 0;

		healthBarPosition = healthBar.transform.localPosition;

		popupText.SetText(text);
		popupText.color = colour;

		StartCoroutine(FadeInAndOut());
	}

	#endregion

	#region Coroutines

	private IEnumerator FadeInAndOut()
	{
		var colour = popupText.color;

		while (colour.a < 1)
		{
			colour.a += (Time.deltaTime / fadeInSeconds);
			popupText.color = colour;

			yield return null;
		}

		yield return new WaitForSeconds(1);

		while (colour.a > 0)
		{
			colour.a -= (Time.deltaTime / fadeOutSeconds);
			popupText.color = colour;

			yield return null;
		}

		Destroy(gameObject);
	}

	#endregion
}
