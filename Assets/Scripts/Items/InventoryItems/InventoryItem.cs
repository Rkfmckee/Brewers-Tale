using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
	#region Fields

	private Slot slotInInventory;

	#endregion

	#region Properties

	public abstract string ItemName { get; }
	public abstract string ItemDescription { get; }

	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	#endregion
}
