using UnityEngine;

public class GameAssetManager : MonoBehaviour
{
	#region Fields

	private static GameAssetManager instance;

	public GameObject HealthPopupPrefab;

	#endregion

	public static GameAssetManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<GameAssetManager>($"Prefabs/Game/Managers/{nameof(GameAssetManager)}"));

			return instance;
		}
	}

	#region Events

	private void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	#endregion
}
