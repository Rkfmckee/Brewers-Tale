using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageInventory : MonoBehaviour
{
	#region Fields

	private InventoryItem itemHeld;
	private InventoryItem itemToSwap;

	private GraphicRaycaster graphicRaycaster;
	private Canvas canvas;
	private GameObject itemHeldPrefab;

	#endregion

	#region Properties

	public InventoryItem ItemHeld { get => itemHeld; set => itemHeld = value; }

	#endregion

	#region Events

	private void Awake()
	{
		References.ManageInventory = this;

		itemHeldPrefab = Resources.Load<GameObject>("Prefabs/Inventory/ItemHeld");
	}

	private void Start()
	{
		canvas           = References.UI.Canvas;
		graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
	}

	private void Update()
	{
		if (!itemHeld)
		{
			PickUpItemIfClicked();
		}
		else
		{
			ItemHeldFollowMouse();
			PutDownItemIfClicked();
		}
	}

	#endregion

	#region Methods

	private void PickUpItemIfClicked()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults   = new List<RaycastResult>();
			var slotClicked      = null as Slot;
			var itemClicked      = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			slotClicked = GetSlot(raycastResults);
			if (!slotClicked) return;

			itemClicked = slotClicked.ItemInSlot;
			if (!itemClicked) return;

			PickUpItem(itemClicked);
		}
	}

	private void PickUpItem(InventoryItem itemClicked)
	{
		itemHeld = itemClicked;
		itemHeld.transform.SetParent(canvas.transform, false);
		itemHeld.transform.position = Input.mousePosition;

		itemClicked.SlotInInventory.ItemInSlot = null;

		// If the item we picked up is from the CraftingResultSlot
		// Remove the crafting ingredients
		if (itemClicked.SlotInInventory is CraftingResultSlot)
		{
			foreach (var craftingSlot in References.CraftingSlots)
			{
				if (!craftingSlot.ItemInSlot) continue;

				Destroy(craftingSlot.ItemInSlot.gameObject);
			}
		}
	}

	private void PutDownItemIfClicked()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults   = new List<RaycastResult>();
			var slotClicked      = null as InventoryCraftingSlot;
			var itemClicked      = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			slotClicked = GetInventorySlot(raycastResults);
			if (!slotClicked) return;

			// If the slot already has an item
			if (slotClicked.ItemInSlot) 
			{
				SwapItems(slotClicked);
				return;
			}
			
			PutDownItem(slotClicked);
		}
	}

	private void SwapItems(InventoryCraftingSlot slot)
	{
		var itemToPickUp = slot.ItemInSlot;

		slot.ItemInSlot = itemHeld;
		itemHeld        = itemToPickUp;

		slot.ItemInSlot.transform.SetParent(slot.transform, false);
		itemHeld.transform.SetParent(canvas.transform, false);
		itemHeld.transform.position = Input.mousePosition;
	}

	private void PutDownItem(InventoryCraftingSlot slot)
	{
		itemHeld.transform.SetParent(slot.transform, false);

		slot.ItemInSlot = itemHeld;
		itemHeld        = null;
	}

	private void ItemHeldFollowMouse()
	{
		if (!itemHeld) return;

		itemHeld.transform.position = Input.mousePosition;
	}

	private Slot GetSlot(List<RaycastResult> results) 
	{
		foreach(var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<Slot>();
			if (inventorySlot) return inventorySlot;
		}

		return null;
	}

	private InventoryCraftingSlot GetInventorySlot(List<RaycastResult> results) 
	{
		foreach(var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<InventoryCraftingSlot>();
			if (inventorySlot) return inventorySlot;
		}

		return null;
	}

	#endregion
}
