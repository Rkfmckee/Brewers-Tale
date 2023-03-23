using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
	#region Constants

	public const float FADE_IN_SECONDS = 0.5f;
	private const float FADE_OUT_SECONDS = 1f;
	private const float SECONDS_TO_SHOW = 3f;

	#endregion

	#region Fields

	private TextMeshProUGUI notificationText;

	#endregion

	#region Events

	private void Awake()
	{
		notificationText = GetComponent<TextMeshProUGUI>();
	}

	#endregion

	#region Methods

	public void Initialize((string, NotificationType) info)
	{
		var text = info.Item1;
		var type = info.Item2;

		notificationText.SetText(text);

		var possibleColour = GetNotificationColour(type);
		if (possibleColour.HasValue)
		{
			var colour = possibleColour.Value;
			colour.a = 0;
			notificationText.color = colour;
		}

		StartCoroutine(FadeInAndOut());
	}

	private Color? GetNotificationColour(NotificationType type)
	{
		switch (type)
		{
			case NotificationType.Success:
				return Color.green;
			case NotificationType.Error:
				return Color.red;
		}

		return null;
	}

	#endregion

	#region Coroutines

	private IEnumerator FadeInAndOut()
	{
		var colour = notificationText.color;

		while (colour.a < 1)
		{
			colour.a += (Time.deltaTime / FADE_IN_SECONDS);
			notificationText.color = colour;

			yield return null;
		}

		yield return new WaitForSeconds(SECONDS_TO_SHOW);

		while (colour.a > 0)
		{
			colour.a -= (Time.deltaTime / FADE_OUT_SECONDS);
			notificationText.color = colour;

			yield return null;
		}

		Destroy(gameObject);
	}

	#endregion
}
