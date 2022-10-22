using UnityEngine;

public class CanvasController : MonoBehaviour
{
    #region Events

	private void Awake()
	{
		References.UI.canvas = GetComponent<Canvas>();
	}

	#endregion
}
