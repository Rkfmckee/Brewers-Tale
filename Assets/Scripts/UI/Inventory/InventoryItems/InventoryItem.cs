using UnityEngine;

public class InventoryItem : MonoBehaviour
{
	#region Fields

	private Slot slotInInventory;

	#endregion

	#region Properties

	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	#endregion
}
