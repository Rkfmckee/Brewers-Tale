using UnityEngine;

public class WorldCanvasManager : MonoBehaviour
{
	#region Fields

	private static WorldCanvasManager instance;

	private Canvas canvas;

	#endregion

	#region Properties

	public static WorldCanvasManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load<WorldCanvasManager>("Prefabs/Game/Managers/WorldCanvasManager"),
				GameObject.Find("GameControllers").transform);

				instance.canvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
			}

			return instance;
		}
	}

	public static Canvas Canvas { get => Instance.canvas; }

	#endregion
}
