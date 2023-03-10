using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationManager : Singleton<NotificationManager>
{
	#region Fields

	public GameObject notificationPrefab;
	public GameObject healthPopupPrefab;

	private List<(string, NotificationType)> notifications;
	private bool showingNotifications;
	private List<(string, NotificationType, HealthBar)> healthPopups;
	private bool showingHealthPopups;
	private TextMeshProUGUI inventoryTitle;
	private Transform titlebar;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		titlebar = WorldCanvasManager.BookCanvasLeft.transform.Find("Titlebar");
		inventoryTitle = titlebar.Find("Title").GetComponent<TextMeshProUGUI>();

		notifications = new List<(string, NotificationType)>();
		healthPopups = new List<(string, NotificationType, HealthBar)>();
		showingNotifications = false;
	}

	#endregion

	#region Methods

	public static void AddNotification(string text)
	{
		NotificationManager.AddNotification(text, NotificationType.Info);
	}

	public static void AddNotification(string text, NotificationType type)
	{
		NotificationManager.Instance.CreateNotification(text, type);
	}

	public static void AddHealthPopup(float amount, NotificationType type, HealthBar healthBar)
	{
		NotificationManager.AddHealthPopup(amount.ToString(), type, healthBar);
	}

	public static void AddHealthPopup(string text, NotificationType type, HealthBar healthBar)
	{
		NotificationManager.Instance.CreateHealthPopup(text, type, healthBar);
	}

	private void CreateNotification(string text, NotificationType type)
	{
		if (notifications.Contains((text, type))) return;

		notifications.Add((text, type));
		if (!showingNotifications) StartCoroutine(ShowingNotifications());
	}

	private void CreateHealthPopup(string text, NotificationType type, HealthBar healthBar)
	{
		healthPopups.Add((text, type, healthBar));
		if (!showingHealthPopups) StartCoroutine(ShowingHealthPopups());
	}

	#endregion

	#region Coroutines

	private IEnumerator ShowingNotifications()
	{
		showingNotifications = true;
		yield return FadeInventoryTitle(true);

		while (notifications.Count > 0)
		{
			var notification = Instantiate(notificationPrefab, titlebar.Find("Notifications")).GetComponent<Notification>();
			var notificationInfo = notifications[0];
			notification.Initialize(notificationInfo);

			while (notification != null)
			{
				yield return null;
			}

			notifications.Remove(notificationInfo);
		}

		yield return FadeInventoryTitle(false);
		showingNotifications = false;
	}

	private IEnumerator ShowingHealthPopups()
	{
		showingHealthPopups = true;

		while (healthPopups.Count > 0)
		{
			var healthPopupInfo = healthPopups[0];
			var healthBar = healthPopupInfo.Item3;
			if (healthBar == null) break;

			var healthPopup = Instantiate(healthPopupPrefab, healthBar.transform).GetComponent<HealthPopup>();
			healthPopup.Initialize(healthPopupInfo);
			healthPopups.Remove(healthPopupInfo);

			yield return new WaitForSeconds(1);
		}

		showingHealthPopups = false;
	}

	private IEnumerator FadeInventoryTitle(bool fadeOut)
	{
		var colour = inventoryTitle.color;
		var fadeSeconds = Notification.FADE_IN_SECONDS;

		if (fadeOut)
		{
			while (colour.a > 0)
			{
				colour.a -= (Time.deltaTime / fadeSeconds);
				inventoryTitle.color = colour;

				yield return null;
			}
		}
		else
		{
			while (colour.a < 1)
			{
				colour.a += (Time.deltaTime / fadeSeconds);
				inventoryTitle.color = colour;

				yield return null;
			}
		}
	}

	#endregion
}
