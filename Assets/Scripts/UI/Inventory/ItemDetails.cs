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
			itemName.text = inventoryItem.ItemName;
			itemDescription.text = inventoryItem.ItemDescription;

			ingredientOptions.gameObject.SetActive(inventoryItem is InventoryIngredient);
			potionOptions.gameObject.SetActive(inventoryItem is InventoryPotion);
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		itemDetails = transform.Find("Details");
		ingredientOptions = transform.Find("IngredientOptions");
		potionOptions = transform.Find("PotionOptions");

		itemName = itemDetails.Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemDescription = itemDetails.Find("ItemDescription").GetComponent<TextMeshProUGUI>();

		var enemyExists = References.Enemies.Count > 0;
		useButton = potionOptions.Find("UseButton").GetComponent<Button>();
		useButton.onClick.AddListener(() => UseItem());
		useButton.interactable = enemyExists;
	}

	private void Start()
	{
		graphicRaycaster = References.UI.Canvas.GetComponent<GraphicRaycaster>();
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

	private void UseItem()
	{
		if (InventoryItem is not InventoryPotion) return;

		var energyCost = (InventoryItem as InventoryPotion).EnergyCost;
		if (References.TurnOrderManager.CurrentEnergy < energyCost)
		{
			print($"Not enough energy to use {InventoryItem.name}");
			return;
		}

		var potionType = InventoryItem.GetComponent<InventoryPotion>().PotionType;
		References.Brewer.TurnAndThrow(potionType);
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
