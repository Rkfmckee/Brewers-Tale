using UnityEngine;
using UnityEngine.UI;

public class Tab : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private GameObject page;
	[SerializeField]
	private Sprite activeSprite;
	[SerializeField]
	private Sprite inactiveSprite;

	private bool isActive;
	private Sprite currentSprite;

	private Image image;

	#endregion

	#region Properties

	public bool IsActive
	{
		get => isActive;
		set
		{
			isActive = value;
			CurrentSprite = isActive ? activeSprite : inactiveSprite;
		}
	}

	public Sprite CurrentSprite
	{
		get => currentSprite;
		set
		{
			currentSprite = value;
			image.sprite = currentSprite;
		}
	}
	public GameObject Page => page;

	#endregion

	#region Events

	private void Awake()
	{
		image = GetComponent<Image>();

		var tabGroup = transform.parent.GetComponent<TabGroup>();
		tabGroup.Subscribe(this);
	}

	#endregion
}
