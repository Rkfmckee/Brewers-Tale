using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	#region Fields

	private GraphicRaycaster graphicRaycaster;
	private Canvas canvas;
	private TurnOrderManager turnOrderManager;

	#endregion

	#region Properties

	public InventoryState ActiveInventory { get; set; }
	public InventoryItem ItemHeld { get; set; }

	#endregion

	#region Events

	private void Awake()
	{
		References.InventoryManager = this;
		ActiveInventory = InventoryState.Inventory;
	}

	private void Start()
	{
		canvas = References.UI.Canvas;
		turnOrderManager = References.TurnOrderManager;

		graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
	}

	private void Update()
	{
		if (ActiveInventory != InventoryState.Inventory) return;

		if (!ItemHeld)
		{
			PickUpItemIfClicked();
			OpenItemOptionsIfClicked();
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
			var raycastResults = new List<RaycastResult>();
			var slotClicked = null as Slot;
			var itemClicked = null as InventoryItem;

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
		ItemHeld = itemClicked;
		ItemHeld.transform.SetParent(canvas.transform, false);
		ItemHeld.transform.position = Input.mousePosition;

		if (!swap) itemClicked.SlotInInventory.ItemInSlot = null;

		itemClicked.SlotInInventory.ItemPickedUp();
	}

	private void OpenItemOptionsIfClicked()
	{
		if (Input.GetButtonDown("Fire2") && References.Brewer.CurrentState == BrewerState.Brewing)
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults = new List<RaycastResult>();
			var slotClicked = null as Slot;
			var itemClicked = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			slotClicked = GetSlot<InventorySlot>(raycastResults);
			if (!slotClicked) return;

			itemClicked = slotClicked.ItemInSlot;
			if (!itemClicked) return;

			OpenItemOptions(itemClicked);
		}
	}

	private void OpenItemOptions(InventoryItem itemClicked)
	{
		var slot = itemClicked.SlotInInventory;
		var itemDetailsPrefab = Resources.Load<GameObject>("Prefabs/UI/Inventory/ItemDetails");
		var spawnPosition = slot.transform.position + itemDetailsPrefab.transform.position;

		var itemDetails = Instantiate(itemDetailsPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<ItemDetails>();
		itemDetails.InventoryItem = itemClicked;
	}

	private void PutDownItemIfClicked()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults = new List<RaycastResult>();
			var slotClicked = null as InventoryCraftingSlot;
			var itemClicked = null as InventoryItem;

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
		if (!slot.CanPlaceItem(ItemHeld)) return;

		slot.ItemInSlot = ItemHeld;
		if (!swap) ItemHeld = null;
	}

	private void SwapItems(InventoryCraftingSlot slot)
	{
		var itemToPickUp = slot.ItemInSlot;

		PutDownItem(slot, true);
		PickUpItem(itemToPickUp, true);
	}

	private void ItemHeldFollowMouse()
	{
		if (!ItemHeld) return;

		ItemHeld.transform.position = Input.mousePosition;
	}

	private T GetSlot<T>(List<RaycastResult> results)
	{
		foreach (var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<T>();
			if (inventorySlot != null) return inventorySlot;
		}

		return default;
	}

	#endregion
}
