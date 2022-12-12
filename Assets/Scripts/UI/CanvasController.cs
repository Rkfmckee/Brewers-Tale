using UnityEngine;

public class CanvasController : MonoBehaviour
{
	#region Events

	private void Awake()
	{
		References.UI.Canvas = GetComponent<Canvas>();
		References.UI.CanvasController = this;
	}

	#endregion
}
