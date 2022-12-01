using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemOptions : MonoBehaviour
{
	#region Fields

	private Button useButton;
	private GraphicRaycaster graphicRaycaster;

	#endregion

	#region Properties

	public InventoryItem InventoryItem { get; set; }

	#endregion

	#region Events

	private void Awake()
	{
		useButton = transform.Find("UseButton").GetComponent<Button>();
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
			if (raycastResults.Count == 0) DestroySelf(true);
		}
	}

	#endregion

	#region Methods

	private void UseItem()
	{
		if (InventoryItem is not InventoryPotion) return;

		var potionType = InventoryItem.GetComponent<InventoryPotion>().PotionType;
		References.Brewer.TurnAndThrow(potionType);

		InventoryItem.SlotInInventory.ItemInSlot = null;
		Destroy(InventoryItem.gameObject);
		DestroySelf(false);
	}

	private void DestroySelf(bool backToInventory)
	{
		References.InventoryManager.ActiveInventory = backToInventory ? InventoryState.Inventory : InventoryState.None;
		Destroy(gameObject);
	}

	#endregion
}
