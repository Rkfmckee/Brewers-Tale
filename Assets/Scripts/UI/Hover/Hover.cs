using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Fields

	[SerializeField]
	private InventoryState activeState;
	[SerializeField]
	private Sprite regularSprite;
	[SerializeField]
	private Sprite highlightSprite;
	private Color highlightColour;
	private Color regularColour;

	private Image image;

	#endregion

	#region Events

	private void Awake()
	{
		image = GetComponent<Image>();

		var transparent = new Color(1, 1, 1, 0);
		var white = new Color(1, 1, 1, 1);

		highlightColour = highlightSprite == null ? transparent : white;
		regularColour = regularSprite == null ? transparent : white;

		image.color = regularColour;
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		if (References.InventoryManager.ActiveInventory != activeState) return;

		image.sprite = highlightSprite;
		image.color = highlightColour;
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		image.sprite = regularSprite;
		image.color = regularColour;
	}

	#endregion
}
