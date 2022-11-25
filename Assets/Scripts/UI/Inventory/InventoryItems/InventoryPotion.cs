using UnityEngine;

public class InventoryPotion : InventoryItem
{
	#region Fields

	[SerializeField]
	private PotionType potionType;

	#endregion

	#region Properties

	public PotionType PotionType { get => potionType; }

	#endregion
}