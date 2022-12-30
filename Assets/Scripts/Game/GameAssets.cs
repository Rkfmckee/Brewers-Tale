using UnityEngine;

public class GameAssets : MonoBehaviour
{
	// Singleton class for holding references to assets which can be accessed anywhere
	// For instance, the HealthPopupPrefab needs to be instantiated through a static method
	// which isn't usually possible

	private static GameAssets instance;

	public static GameAssets Instance
	{
		get
		{
			if (instance == null) instance = Instantiate(Resources.Load<GameAssets>("Prefabs/Game/GameAssets"));
			return instance;
		}
	}

	public GameObject HealthPopupPrefab;
}
