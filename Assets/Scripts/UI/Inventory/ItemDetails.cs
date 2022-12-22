using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDetails : MonoBehaviour
{
	#region Fields

	private TextMeshProUGUI itemName;
	private TextMeshProUGUI itemDescription;
	private Button useButton;
	private GraphicRaycaster graphicRaycaster;

	#endregion

	#region Properties

	public InventoryItem InventoryItem { get; set; }
	public string ItemName { set => itemName.text = value; }
	public string ItemDescription { set => itemDescription.text = value; }


	#endregion

	#region Events

	private void Awake()
	{
		itemName = transform.Find("Details").Find("ItemName").GetComponent<TextMeshProUGUI>();
		itemDescription = transform.Find("Details").Find("ItemDescription").GetComponent<TextMeshProUGUI>();

		useButton = transform.Find("Options").Find("UseButton").GetComponent<Button>();
		useButton.onClick.AddListener(() => UseItem());
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

			pointerEventData.position = Input.mousePosition;
			graphicRaycaster.Raycast(pointerEventData, raycastResults);

			foreach (var result in raycastResults)
			{
				if (!GameObject.ReferenceEquals(result.gameObject, gameObject))
				{
					resultsToRemove.Add(result);
				}
			}

			raycastResults.RemoveAll(i => resultsToRemove.Contains(i));
			if (raycastResults.Count == 0) DestroySelf();
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
