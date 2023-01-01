using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
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
	}

	private void Start()
	{
		graphicRaycaster = References.UI.OverlayCanvas.GetComponent<GraphicRaycaster>();
		References.InventoryManager.ActiveInventory = InventoryState.ItemDetails;
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

		if (inventoryItem.SlotInInventory is not InventorySlot)
		{
			ingredientOptions.gameObject.SetActive(false);
			potionOptions.gameObject.SetActive(false);
		}

		UpdatePosition();
	}

	private void UpdatePosition()
	{
		var slotPosition = InventoryItem.SlotInInventory.transform.position;

		if (slotPosition.x > (Screen.width / 2))
		{
			var offsetPosition = rectTransform.position - slotPosition;
			offsetPosition.x *= -1;
			rectTransform.position = slotPosition + offsetPosition;
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

		if (References.TurnOrderManager.CurrentEnergy < energyCost)
		{
			print($"Not enough energy to use {InventoryItem.ItemName}");
			return;
		}

		var potionToThrow = inventoryPotion.WorldPrefab;
		References.Brewer.TurnAndThrow(potionToThrow);
		References.TurnOrderManager.CurrentEnergy -= energyCost;

		InventoryItem.SlotInInventory.ItemInSlot = null;
		Destroy(InventoryItem.gameObject);
		DestroySelf();
	}

	private void DestroySelf()
	{
		References.InventoryManager.ActiveInventory = InventoryState.Inventory;
		Destroy(gameObject);
	}

	#endregion
}
