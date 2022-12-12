using UnityEngine;

public class InventoryPotion : InventoryItem
{
	#region Fields

	[SerializeField]
	private PotionType potionType;
	[SerializeField]
	[Range(1, 3)]
	private int energyCost;

	#endregion

	#region Properties

	public PotionType PotionType { get => potionType; }
	public int EnergyCost { get => energyCost; }

	#endregion
}