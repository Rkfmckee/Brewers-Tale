using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour
{
	#region Fields

	protected static T instance;

	#endregion

	#region Properties

	public static T Instance
	{
		get
		{
			if (instance == null)
			{
				instance = Instantiate(Resources.Load<GameObject>($"Prefabs/Game/Managers/{typeof(T).Name}")).GetComponent<T>();
			}

			return instance;
		}
	}

	#endregion

	#region Events

	protected virtual void Awake()
	{
		DontDestroyOnLoad(gameObject);
	}

	#endregion

	#region Methods

	public virtual void Initialize() { }

	#endregion
}