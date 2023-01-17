using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryInitializer : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private List<InventoryItem> startingItems;

	private GameObject inventorySlotPrefab;
	private Transform inventorySlotGroup;
	private int numberOfSlots;

	#endregion

	#region Events

	private void Awake()
	{
		inventorySlotPrefab = Resources.Load<GameObject>("Prefabs/UI/Inventory/InventorySlot");
		inventorySlotGroup = WorldCanvasManager.BookCanvasLeft.transform.Find("Inventory").Find("InventorySlots");
		numberOfSlots = 34;
	}

	private void Start()
	{
		CreateInventorySlots();
	}

	#endregion

	#region Methods

	private void CreateInventorySlots()
	{
		if (startingItems.Count > numberOfSlots) print($"Can't start with {startingItems.Count}, but only {numberOfSlots} slots.");

		if (References.Inventory.Slots.Count > 0)
		{
			print("Slots already exist");
			return;
		}

		var slotNumber = 1;
		for (int i = 0; i < numberOfSlots; i++)
		{
			var inventorySlot = Instantiate(inventorySlotPrefab, inventorySlotGroup).GetComponent<InventorySlot>();
			inventorySlot.SlotNumber = slotNumber;
			slotNumber++;

			if (startingItems.Count == 0) continue;
			inventorySlot.ItemInSlot = startingItems[0];
			startingItems.RemoveAt(0);
		}

		References.Inventory.Slots = References.Inventory.Slots.OrderBy(s => s.SlotNumber).ToList();
	}

	#endregion
}
