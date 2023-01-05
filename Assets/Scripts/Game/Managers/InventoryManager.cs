using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
	#region Fields

	private static InventoryManager instance;

	private InventoryItem itemHeld;
	private RectTransform itemHeldTransform;
	private Vector2 itemHeldPosition;
	private ScreenSide mouseSide;

	private Canvas canvas;
	private RectTransform canvasTransform;
	private GraphicRaycaster graphicRaycaster;
	private new Camera camera;

	#endregion

	#region Properties

	public static InventoryManager Instance
	{
		get
		{
			if (instance == null)
				instance = Instantiate(Resources.Load<InventoryManager>($"Prefabs/Game/Managers/{nameof(InventoryManager)}"));

			return instance;
		}
	}

	public InventoryState ActiveInventory { get; set; }
	public InventoryItem ItemHeld
	{
		get => itemHeld;
		set
		{
			itemHeld = value;
			if (itemHeld != null) itemHeldTransform = itemHeld.GetComponent<RectTransform>();
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		ActiveInventory = InventoryState.Inventory;
		camera = Camera.main;
	}

	private void Start()
	{
		canvas = WorldCanvasManager.BookCanvasLeft;
		canvasTransform = canvas.GetComponent<RectTransform>();
		graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
	}

	private void Update()
	{
		if (ActiveInventory != InventoryState.Inventory) return;

		GetMouseScreenSide();

		if (!ItemHeld)
		{
			PickUpItemIfClicked();
			OpenItemOptionsIfClicked();
		}
		else
		{
			SetPositionOfItem();
			PutDownItemIfClicked();
		}
	}

	#endregion

	#region Methods

	private void GetMouseScreenSide()
	{
		var mousePositionX = Input.mousePosition.x;
		var mouseOnLeftSide = mousePositionX < Screen.width / 2;

		if ((mouseSide == ScreenSide.Left) && !mouseOnLeftSide)
		{
			mouseSide = ScreenSide.Right;

			canvas = WorldCanvasManager.BookCanvasRight;
			canvasTransform = canvas.GetComponent<RectTransform>();
			graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

			return;
		}

		if (mouseSide == ScreenSide.Right && mouseOnLeftSide)
		{
			mouseSide = ScreenSide.Left;

			canvas = WorldCanvasManager.BookCanvasLeft;
			canvasTransform = canvas.GetComponent<RectTransform>();
			graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();

			return;
		}
	}

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
		SetPositionOfItem();

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

			slotClicked = GetSlot<Slot>(raycastResults);
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

	private void SetPositionOfItem()
	{
		if (itemHeldPosition == null) return;

		if (itemHeldTransform.parent != canvasTransform) itemHeldTransform.SetParent(canvasTransform);

		RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasTransform, Input.mousePosition, camera, out itemHeldPosition);
		itemHeldTransform.localPosition = new Vector3(itemHeldPosition.x, itemHeldPosition.y, 0);
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
