using UnityEngine;

public class WorldCanvasManager : MonoBehaviour
{
	#region Fields

	private static WorldCanvasManager instance;

	private Canvas canvas;
	private Canvas bookCanvasLeft;
	private Canvas bookCanvasRight;

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
				instance.bookCanvasLeft = GameObject.Find("Book").transform.Find("CanvasLeft").GetComponent<Canvas>();
				instance.bookCanvasRight = GameObject.Find("Book").transform.Find("CanvasRight").GetComponent<Canvas>();
			}

			return instance;
		}
	}

	public static Canvas Canvas { get => Instance.canvas; }
	public static Canvas BookCanvasLeft { get => Instance.bookCanvasLeft; }
	public static Canvas BookCanvasRight { get => Instance.bookCanvasRight; }

	#endregion

	#region Events

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	#endregion
}
