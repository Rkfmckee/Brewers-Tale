using System.Collections.Generic;

public class BoneShards : InventoryIngredient, IItemEffect
{
	public override string ItemName => "Bone shards";
	public override string IngredientDescription => $"They're sharp.";
	public override string EffectDescription => $"Low Physical damage";

	public List<Damage> Damage => new List<Damage> { new Damage(2, DamageType.Physical) };
	public Condition Condition => null;
}