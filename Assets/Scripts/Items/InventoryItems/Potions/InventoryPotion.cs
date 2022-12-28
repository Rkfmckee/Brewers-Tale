using UnityEngine;

public class InventoryPotion : InventoryItem
{
	#region Fields

	[SerializeField]
	[Range(1, 3)]
	private int energyCost;
	[SerializeField]
	private GameObject worldPrefab;

	#endregion

	#region Properties

	public int EnergyCost { get => energyCost; }
	public GameObject WorldPrefab { get => worldPrefab; }

	#endregion
}