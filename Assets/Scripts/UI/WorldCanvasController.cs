using UnityEngine;

public class WorldCanvasController : MonoBehaviour
{
	#region Events

	private void Awake()
	{
		References.UI.WorldCanvas = GetComponent<Canvas>();
		References.UI.WorldCanvasController = this;
	}

	#endregion
}
