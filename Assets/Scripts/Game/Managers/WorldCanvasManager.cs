using UnityEngine;

public class WorldCanvasManager : MonoBehaviour
{
	#region Fields

	private static WorldCanvasManager instance;

	private Canvas canvas;
	private Canvas bookCanvas;

	#endregion

	#region Properties

	public static WorldCanvasManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load<WorldCanvasManager>($"Prefabs/Game/Managers/{nameof(WorldCanvasManager)}"));
				instance.canvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
				instance.bookCanvas = GameObject.Find("Book").transform.Find("Canvas").GetComponent<Canvas>();
			}

			return instance;
		}
	}

	public static Canvas Canvas { get => Instance.canvas; }
	public static Canvas BookCanvas { get => Instance.bookCanvas; }

	#endregion

	#region Events

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	#endregion
}
