using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    #region Fields

	[SerializeField]
	private Sprite inventoryIcon;

	private Slot slotInInventory;

	#endregion

	#region Properties

	public Sprite InventoryIcon { get => inventoryIcon; set => inventoryIcon = value; }

	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	#endregion
}
