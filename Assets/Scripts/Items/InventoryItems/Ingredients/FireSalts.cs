public class FireSalts : InventoryIngredient, IItemEffect
{
	public override string IngredientName => "Fire salts";
	public override string AestheticDescription => "Burning remnants of magical flame.";
	public override string EffectDescription => $"Low {Damage.Type} damage, chance of {Condition.Name}.";

	public Damage Damage => new Damage(2, DamageType.Fire);
	public Condition Condition => new Burning(Damage.Amount, 3);
}