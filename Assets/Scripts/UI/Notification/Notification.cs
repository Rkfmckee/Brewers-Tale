using System.Collections;
using TMPro;
using UnityEngine;

public class Notification : MonoBehaviour
{
	#region Fields

	private float fadeInSeconds;
	private float fadeOutSeconds;
	private float secondsToShow;

	private TextMeshProUGUI notificationText;

	#endregion

	#region Events

	private void Awake()
	{
		notificationText = GetComponent<TextMeshProUGUI>();

		fadeInSeconds = 0.5f;
		fadeOutSeconds = 1;
		secondsToShow = 3;
	}

	#endregion

	#region Methods

	public void Initialize((string, NotificationType) info)
	{
		var text = info.Item1;
		var type = info.Item2;

		var colour = GetNotificationColour(type);
		colour.a = 0;

		notificationText.SetText(text);
		notificationText.color = colour;

		StartCoroutine(FadeInAndOut());
	}

	private Color GetNotificationColour(NotificationType type)
	{
		switch (type)
		{
			case NotificationType.Success:
				return Color.green;
			case NotificationType.Error:
				return Color.red;
		}

		return Color.white;
	}

	#endregion

	#region Coroutines

	private IEnumerator FadeInAndOut()
	{
		var colour = notificationText.color;

		while (colour.a < 1)
		{
			colour.a += (Time.deltaTime / fadeInSeconds);
			notificationText.color = colour;

			yield return null;
		}

		yield return new WaitForSeconds(secondsToShow);

		while (colour.a > 0)
		{
			colour.a -= (Time.deltaTime / fadeOutSeconds);
			notificationText.color = colour;

			yield return null;
		}

		Destroy(gameObject);
	}

	#endregion
}