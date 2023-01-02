using TMPro;
using UnityEngine;

public class OverlayCanvasManager : MonoBehaviour
{
	#region Fields

	private static OverlayCanvasManager instance;

	public Canvas canvas;

	#endregion

	#region Properties

	public static OverlayCanvasManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<OverlayCanvasManager>("Prefabs/Game/OverlayCanvasManager"),
				GameObject.Find("GameControllers").transform);

			return instance;
		}
	}

	public static Canvas Canvas { get => Instance.canvas; }

	#endregion
}
