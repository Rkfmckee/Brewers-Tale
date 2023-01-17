using UnityEngine;

public class OverlayCanvasManager : Singleton<OverlayCanvasManager>
{
	#region Fields

	private Canvas canvas;

	#endregion

	#region Properties

	public static Canvas Canvas { get => Instance.canvas; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		canvas = GameObject.Find("OverlayCanvas").GetComponent<Canvas>();
	}

	#endregion
}
