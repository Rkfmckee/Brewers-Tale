public class BoneShards : InventoryIngredient
{
	public override string IngredientName => "Bone shards";
	public override string AestheticDescription => $"They're sharp.";
	public override string EffectDescription => $"Low {Damage.Type} damage";
	public override Damage Damage => new Damage(2, DamageType.Physical);
}