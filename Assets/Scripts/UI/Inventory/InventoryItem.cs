using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    #region Fields

	[SerializeField]
	private Sprite inventoryIcon;

	#endregion

	#region Properties

	public Sprite InventoryIcon { get => inventoryIcon; set => inventoryIcon = value; }

	#endregion
}
