using UnityEngine;

public class InventoryFrostPotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Frost potion";
	public override string PotionDescription => "Deals cold damage, with % chance to Slow";
	public override int EnergyCost => 1;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldFrostPotion");

	#endregion
}