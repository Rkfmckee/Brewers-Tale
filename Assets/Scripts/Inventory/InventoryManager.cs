using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	#region Fields

	private InventoryItem itemHeld;

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
		References.InventoryManager = this;

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

			slotClicked = GetSlot<Slot>(raycastResults);
			if (!slotClicked) return;

			itemClicked = slotClicked.ItemInSlot;
			if (!itemClicked) return;

			PickUpItem(itemClicked);
		}
	}

	private void PickUpItem(InventoryItem itemClicked, bool swap = false)
	{
		itemHeld = itemClicked;
		itemHeld.transform.SetParent(canvas.transform, false);
		itemHeld.transform.position = Input.mousePosition;

		if (!swap) itemClicked.SlotInInventory.ItemInSlot = null;

		itemClicked.SlotInInventory.ItemPickedUp();
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

			slotClicked = GetSlot<InventoryCraftingSlot>(raycastResults);
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

	private void PutDownItem(InventoryCraftingSlot slot, bool swap = false)
	{
		slot.ItemInSlot  	= itemHeld;
		if (!swap) itemHeld = null;
	}

	private void SwapItems(InventoryCraftingSlot slot)
	{
		var itemToPickUp = slot.ItemInSlot;

		PutDownItem(slot, true);
		PickUpItem(itemToPickUp, true);
	}

	private void ItemHeldFollowMouse()
	{
		if (!itemHeld) return;

		itemHeld.transform.position = Input.mousePosition;
	}

	private T GetSlot<T>(List<RaycastResult> results) 
	{
		foreach(var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<T>();
			if (inventorySlot != null) return inventorySlot;
		}

		return default;
	}

	#endregion
}
