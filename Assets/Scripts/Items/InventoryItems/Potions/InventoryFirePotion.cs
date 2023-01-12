using UnityEngine;

public class InventoryFirePotion : InventoryPotion
{
	#region Properties

	public override string PotionName => "Fire potion";
	public override string PotionDescription => "Deals fire damage, with % chance for Burning";
	public override int EnergyCost => 1;
	public override GameObject WorldPrefab => Resources.Load<GameObject>("Prefabs/Items/WorldItems/WorldFirePotion");

	#endregion
}