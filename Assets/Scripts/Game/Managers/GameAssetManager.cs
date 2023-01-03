using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
	// Singleton class for holding references to assets which can be accessed anywhere
	// For instance, the HealthPopupPrefab needs to be instantiated through a static method
	// which isn't usually possible

	private static GameAssetManager instance;

	public static GameAssetManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<GameAssetManager>($"Prefabs/Game/Managers/{nameof(GameAssetManager)}"),
				GameObject.Find("GameControllers").transform);

			return instance;
		}
	}

	public GameObject HealthPopupPrefab;
}
