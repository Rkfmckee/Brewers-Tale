using UnityEngine.EventSystems;

public class HoverCraftingResult : Hover
{
	#region Events

	public override void OnPointerEnter(PointerEventData eventData)
	{
		var shouldHightlight = !References.ManageInventory.ItemHeld && References.CraftingResultSlot.ItemInSlot;
		if (shouldHightlight) base.OnPointerEnter(eventData);
	}

	#endregion
}