using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private string itemName;
	[SerializeField]
	private string itemDescription;

	private Slot slotInInventory;

	#endregion

	#region Properties

	public string ItemName { get => itemName; }
	public string ItemDescription { get => itemDescription; }
	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	#endregion
}
