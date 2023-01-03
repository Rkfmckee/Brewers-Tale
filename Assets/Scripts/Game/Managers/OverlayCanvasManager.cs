using UnityEngine;

public class OverlayCanvasManager : MonoBehaviour
{
	#region Fields

	private static OverlayCanvasManager instance;

	private Canvas canvas;

	#endregion

	#region Properties

	public static OverlayCanvasManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load<OverlayCanvasManager>("Prefabs/Game/Managers/OverlayCanvasManager"),
				GameObject.Find("GameControllers").transform);

				instance.canvas = GameObject.Find("OverlayCanvas").GetComponent<Canvas>();
			}

			return instance;
		}
	}

	public static Canvas Canvas { get => Instance.canvas; }

	#endregion
}
