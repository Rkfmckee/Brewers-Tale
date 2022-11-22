using UnityEngine.EventSystems;

public class HoverCraftingResult : Hover
{
	#region Events

	public override void OnPointerEnter(PointerEventData eventData)
	{
		var shouldHightlight = !References.InventoryManager.ItemHeld && References.Crafting.ResultSlot.ItemInSlot;
		if (shouldHightlight) base.OnPointerEnter(eventData);
	}

	#endregion
}