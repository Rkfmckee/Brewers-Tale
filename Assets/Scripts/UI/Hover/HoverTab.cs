using UnityEngine.EventSystems;

public class HoverTab : Hover
{
	#region Fields

	private Tab tab;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		tab = GetComponent<Tab>();
	}

	public override void OnPointerEnter(PointerEventData eventData)
	{
		if (tab.IsActive) return;

		base.OnPointerEnter(eventData);
	}

	public override void OnPointerExit(PointerEventData eventData)
	{
		image.sprite = tab.CurrentSprite;
		image.color = regularColour;
	}

	#endregion
}
