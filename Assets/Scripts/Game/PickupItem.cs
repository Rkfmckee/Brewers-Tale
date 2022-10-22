using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PickUpItem : MonoBehaviour
{
	#region Fields

	private InventoryItem itemHeld;
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
		canvas = References.UI.canvas;
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
			var inventorySlotClicked = null as InventorySlot;
			var itemClicked          = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			inventorySlotClicked = GetInventorySlot(raycastResults);
			if (!inventorySlotClicked) return;

			itemClicked = inventorySlotClicked.ItemInSlot;
			if (!itemClicked) return;

			// Pick up the item, and remove it from the inventory slot
			itemHeld = itemClicked;
			itemHeldObject = Instantiate(itemHeldPrefab, Input.mousePosition, Quaternion.identity, canvas.transform);
			itemHeldObject.GetComponent<Image>().sprite = itemHeld.InventoryIcon;
			inventorySlotClicked.ItemInSlot = null;
		}
	}

	private void ItemHeldFollowMouse()
	{
		if (!itemHeld || !itemHeldObject) return;

		itemHeldObject.transform.position = Input.mousePosition;
	}

	private void PutDownItemIfClicked()
	{
		if (Input.GetButtonDown("Fire1")) 
		{
			var pointerEventData     = new PointerEventData(null);
			var raycastResults       = new List<RaycastResult>();
			var inventorySlotClicked = null as InventorySlot;
			var itemClicked          = null as InventoryItem;

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			inventorySlotClicked = GetInventorySlot(raycastResults);
			if (!inventorySlotClicked) return;
			if (inventorySlotClicked.ItemInSlot) return;
			
			// Put down the item
			inventorySlotClicked.ItemInSlot = itemHeld;
			itemHeld = null;
			Destroy(itemHeldObject);
		}
	}

	private InventorySlot GetInventorySlot(List<RaycastResult> results) 
	{
		foreach(var result in results)
		{
			var inventorySlot = result.gameObject.GetComponent<InventorySlot>();
			if (inventorySlot) return inventorySlot;
		}

		return null;
	}

	#endregion
}
