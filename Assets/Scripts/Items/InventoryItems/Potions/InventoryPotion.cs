using UnityEngine;

public class InventoryPotion : InventoryItem
{
	#region Properties

	public virtual string PotionName { get; }
	public virtual string PotionDescription { get; }
	public virtual int EnergyCost { get; }
	public virtual GameObject WorldPrefab { get; }

	#endregion
}