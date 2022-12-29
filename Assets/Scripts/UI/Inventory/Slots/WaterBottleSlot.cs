using UnityEngine;

public class WaterBottleSlot : Slot
{
	#region Fields

	private InventoryItem waterBottle;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		waterBottle = Resources.Load<GameObject>("Prefabs/Items/InventoryItems/Ingredients/WaterBottle").GetComponent<InventoryItem>();
		ItemInSlot = waterBottle;
	}

	#endregion

	#region Methods

	public override void ItemPickedUp()
	{
		ItemInSlot = waterBottle;
	}

	#endregion
}
