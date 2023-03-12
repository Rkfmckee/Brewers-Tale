using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
	#region Constants

	public const float X_POSITION_OFFSET = 30;

	#endregion

	#region Fields

	private InventoryItem inventoryItem;

	private Transform itemDetails;
	private Transform ingredientOptions;
	private Transform potionOptions;
	private Transform ingredientLabel;
	private Transform potionLabel;
	private RectTransform rectTransform;
	private RectTransform ingredientRectTransform;
	private RectTransform potionRectTransform;
	private TextMeshProUGUI itemName;
	private TextMeshProUGUI itemDescription;
	private Button useButton;
	private GraphicRaycaster graphicRaycaster;
	private new Camera camera;

	#endregion

	#region Properties

	public InventoryItem InventoryItem
	{
		get => inventoryItem;
		set
		{
			inventoryItem = value;
			UpdateItemDetails();
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		itemDetails = transform.Find("Details");
		ingredientOptions = transform.Find("IngredientOptions");
		potionOptions = transform.Find("PotionOptions");
		ingredientLabel = itemDetails.Find("Ingredient");
		potionLabel = itemDetails.Find("Potion");

		rectTransform = GetComponent<RectTransform>();
		ingredientRectTransform = ingredientOptions.GetComponent<RectTransform>();
		potionRectTransform = potionOptions.GetComponent<RectTransform>();

		itemName = itemDetails.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemDescription = itemDetails.Find("ItemDescription").GetComponent<TextMeshProUGUI>();

		var enemyExists = References.Enemies.Count > 0;
		useButton = potionOptions.Find("UseButton").GetComponent<Button>();
		useButton.onClick.AddListener(() => UseItem());
		useButton.interactable = enemyExists;

		graphicRaycaster = GetComponent<GraphicRaycaster>();
		camera = Camera.main;

		InventoryManager.Instance.ActiveInventory = InventoryState.ItemDetails;
	}

	private void Update()
	{
		if (Input.GetButtonDown("Fire1"))
		{
			var pointerEventData = new PointerEventData(null);
			var raycastResults = new List<RaycastResult>();
			var resultsToRemove = new List<RaycastResult>();
			var itemDetailsParts = new List<GameObject>
			{
				itemDetails.gameObject,
				ingredientOptions.gameObject,
				potionOptions.gameObject
			};

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			var hasClickedThis = raycastResults.Any(r => itemDetailsParts.Contains(r.gameObject));
			if (!hasClickedThis) DestroySelf();
		}
	}

	#endregion

	#region Methods

	private void UpdateItemDetails()
	{
		itemName.SetText(inventoryItem.ItemName);
		itemDescription.SetText(inventoryItem.ItemDescription);

		var isIngredient = inventoryItem is InventoryIngredient;
		ingredientLabel.gameObject.SetActive(isIngredient);
		ingredientOptions.gameObject.SetActive(isIngredient);

		var isPotion = inventoryItem is InventoryPotion;
		potionLabel.gameObject.SetActive(isPotion);
		potionOptions.gameObject.SetActive(isPotion);

		if (isPotion) potionLabel.GetComponent<TextMeshProUGUI>().text = $"Potion: {(inventoryItem as InventoryPotion).EnergyCost}";

		if (inventoryItem.SlotInInventory is not InventorySlot)
		{
			ingredientOptions.gameObject.SetActive(false);
			potionOptions.gameObject.SetActive(false);
		}

		UpdatePosition();
	}

	private void UpdatePosition()
	{
		// If the slot is on the right side of a page,
		// Move the ItemDetails to the left side of the slot.
		// The right side of the page is the 2nd or 4th quarter
		// of the screen.

		var slotPosition = InventoryItem.SlotInInventory.transform.position;
		var quarterScreenWidth = Screen.width / 4;
		var quarterOfSlot = Mathf.Ceil(camera.WorldToScreenPoint(slotPosition).x / quarterScreenWidth);

		if (quarterOfSlot == 2 || quarterOfSlot == 4)
		{
			rectTransform.localPosition *= -1;
			rectTransform.pivot = new Vector2(1, rectTransform.pivot.y);

			ingredientRectTransform.anchorMin = new Vector2(1, ingredientRectTransform.anchorMin.y);
			ingredientRectTransform.anchorMax = new Vector2(1, ingredientRectTransform.anchorMax.y);
			ingredientRectTransform.pivot = new Vector2(1, ingredientRectTransform.pivot.y);

			potionRectTransform.anchorMin = new Vector2(1, potionRectTransform.anchorMin.y);
			potionRectTransform.anchorMax = new Vector2(1, potionRectTransform.anchorMax.y);
			potionRectTransform.pivot = new Vector2(1, potionRectTransform.pivot.y);
		}
	}

	private void UseItem()
	{
		if (InventoryItem is not InventoryPotion) return;

		var inventoryPotion = InventoryItem as InventoryPotion;
		var energyCost = inventoryPotion.EnergyCost;

		if (!EnergyManager.HaveEnoughEnergy(energyCost, $"use {InventoryItem.ItemName}")) return;

		References.Brewer.TurnAndThrow(inventoryPotion);

		InventoryItem.SlotInInventory.ItemInSlot = null;
		Destroy(InventoryItem.gameObject);
		DestroySelf();
	}

	private void DestroySelf()
	{
		InventoryManager.Instance.ActiveInventory = InventoryState.Inventory;
		Destroy(gameObject);
	}

	#endregion
}
