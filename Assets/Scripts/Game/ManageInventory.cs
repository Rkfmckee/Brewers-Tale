using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ManageInventory : MonoBehaviour
{
	#region Fields

	private InventoryItem itemHeld;
	private InventoryItem itemToSwap;
	private GameObject itemHeldObject;

	private GraphicRaycaster graphicRaycaster;
	private Canvas canvas;
	private GameObject itemHeldPrefab;

	#endregion

	#region Events

	private void Awake() {
		itemHeldPrefab = Resources.Load<GameObject>("Prefabs/Inventory/ItemHeld");
	}

	private void Start()
	{
		canvas           = References.UI.canvas;
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
			var pointerEventData     = new PointerEventData(null);
			var raycastResults       = new List<RaycastResult>();
			var inventorySlotClicked = null as Slot;
			var itemClicked          = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			inventorySlotClicked = GetInventorySlot(raycastResults);
			if (!inventorySlotClicked) return;

			itemClicked = inventorySlotClicked.ItemInSlot;
			if (!itemClicked) return;

			PickUpItem(itemClicked);
		}
	}

	private void PickUpItem(InventoryItem itemClicked)
	{
		// If we're already holding an item, put down the current one first
		// Otherwise, we'll just be setting ItemInSlot to null
		itemClicked.SlotInInventory.ItemInSlot = itemHeld;
		itemHeld = itemClicked;

		if (!itemHeldObject) itemHeldObject = Instantiate(itemHeldPrefab, Input.mousePosition, Quaternion.identity, canvas.transform);
		itemHeldObject.GetComponent<Image>().sprite = itemHeld.InventoryIcon;
	}

	private void PutDownItemIfClicked()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults   = new List<RaycastResult>();
			var slotClicked      = null as Slot;
			var itemClicked      = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			slotClicked = GetInventorySlot(raycastResults);
			if (!slotClicked) return;

			// If the slot already has an item,
			if (slotClicked.ItemInSlot) 
			{
				// Put this one down and pick up that one instead
				PickUpItem(slotClicked.ItemInSlot);
				return;
			}
			
			PutDownItem(slotClicked);
		}
	}

	private void PutDownItem(Slot slot)
	{
		slot.ItemInSlot = itemHeld;
		itemHeld        = null;
		Destroy(itemHeldObject);
	}

	private void ItemHeldFollowMouse()
	{
		if (!itemHeld || !itemHeldObject) return;

		itemHeldObject.transform.position = Input.mousePosition;
	}

	private Slot GetInventorySlot(List<RaycastResult> results) 
	{
		foreach(var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<Slot>();
			if (inventorySlot) return inventorySlot;
		}

		return null;
	}

	#endregion
}
