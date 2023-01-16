using UnityEngine;

public class WorldCanvasManager : Singleton<WorldCanvasManager>
{
	#region Fields

	private Canvas canvas;
	private Canvas bookCanvasLeft;
	private Canvas bookCanvasRight;

	#endregion

	#region Properties

	public static Canvas Canvas { get => Instance.canvas; }
	public static Canvas BookCanvasLeft { get => Instance.bookCanvasLeft; }
	public static Canvas BookCanvasRight { get => Instance.bookCanvasRight; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		var book = GameObject.Find("Book");
		canvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
		bookCanvasLeft = book.transform.Find("CanvasLeft").GetComponent<Canvas>();
		bookCanvasRight = book.transform.Find("CanvasRight").GetComponent<Canvas>();
	}

	#endregion
}
