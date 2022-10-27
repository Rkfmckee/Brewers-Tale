using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    #region Fields

	[SerializeField]
	private Sprite inventoryIcon;
	private InventorySlot inventorySlot;

	#endregion

	#region Properties

	public Sprite InventoryIcon { get => inventoryIcon; set => inventoryIcon = value; }

	public InventorySlot InventorySlot { get => inventorySlot; set => inventorySlot = value; }

	#endregion
}
