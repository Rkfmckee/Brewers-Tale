using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	#region Fields

	[SerializeField]
	private Sprite regularSprite;
	[SerializeField]
	private Sprite highlightSprite;

	private Image image;

	#endregion

	#region Events

	private void Awake()
	{
		image = GetComponent<Image>();
	}

	public virtual void OnPointerEnter(PointerEventData eventData)
	{
		image.sprite = highlightSprite;
	}

	public virtual void OnPointerExit(PointerEventData eventData)
	{
		image.sprite = regularSprite;
	}

	#endregion
}
