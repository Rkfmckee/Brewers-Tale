using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour
{
	#region Fields

	private static NotificationManager instance;

	public GameObject notificationPrefab;

	private List<(string, NotificationType)> notifications;
	private bool showingNotifications;

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
		showingNotifications = false;
	}

	#endregion

	#region Methods

	public static void Add(string text)
	{
		NotificationManager.Instance.AddNotification(text, NotificationType.Info);
	}

	public static void Add(string text, NotificationType type)
	{
		NotificationManager.Instance.AddNotification(text, type);
	}

	public void AddNotification(string text, NotificationType type)
	{
		if (notifications.Contains((text, type))) return;

		notifications.Add((text, type));
		if (!showingNotifications) StartCoroutine(ShowingNotifications());
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

	#endregion
}
