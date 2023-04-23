using System.Collections.Generic;

public class BoneShards : InventoryIngredient, IItemEffect
{
	public override string IngredientName => "Bone shards";
	public override string AestheticDescription => $"They're sharp.";
	public override string EffectDescription => $"Low {Damage.Type} damage";

	public Damage Damage => new Damage(2, DamageType.Physical);
	public Condition Condition => null;
}