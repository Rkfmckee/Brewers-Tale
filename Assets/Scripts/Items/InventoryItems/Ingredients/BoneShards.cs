using System.Collections.Generic;

public class BoneShards : InventoryIngredient, IItemEffect
{
	public override string ItemName => "Bone shards";
	public override string IngredientDescription => $"They're sharp.";

	public List<Damage> Damage => new List<Damage> { new Damage(2, DamageType.Piercing) };
	public Condition Condition => null;
}