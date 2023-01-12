using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	#region Fields

	private static NotificationManager instance;

	public GameObject notificationPrefab;
	public GameObject healthPopupPrefab;

	private List<(string, NotificationType)> notifications;
	private bool showingNotifications;
	private List<(string, NotificationType, HealthBar)> healthPopups;
	private bool showingHealthPopups;

	#endregion

	#region Properties

	public static NotificationManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<NotificationManager>($"Prefabs/Game/Managers/{nameof(NotificationManager)}"));

			return instance;
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);

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
		NotificationManager.Instance.Notification(text, type);
	}

	public static void AddHealthPopup(float amount, NotificationType type, HealthBar healthBar)
	{
		NotificationManager.AddHealthPopup(amount.ToString(), type, healthBar);
	}

	public static void AddHealthPopup(string text, NotificationType type, HealthBar healthBar)
	{
		NotificationManager.Instance.HealthPopup(text, type, healthBar);
	}

	private void Notification(string text, NotificationType type)
	{
		if (notifications.Contains((text, type))) return;

		notifications.Add((text, type));
		if (!showingNotifications) StartCoroutine(ShowingNotifications());
	}

	private void HealthPopup(string text, NotificationType type, HealthBar healthBar)
	{
		healthPopups.Add((text, type, healthBar));
		if (!showingHealthPopups) StartCoroutine(ShowingHealthPopups());
	}

	#endregion

	#region Coroutines

	private IEnumerator ShowingNotifications()
	{
		showingNotifications = true;

		while (notifications.Count > 0)
		{
			var notification = Instantiate(notificationPrefab, WorldCanvasManager.BookCanvasRight.transform.Find("Notifications")).GetComponent<Notification>();
			var notificationInfo = notifications[0];
			notification.Initialize(notificationInfo);

			while (notification != null)
			{
				yield return null;
			}

			notifications.Remove(notificationInfo);
		}

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

	#endregion
}
