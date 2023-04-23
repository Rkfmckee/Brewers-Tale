using System.Collections.Generic;

public class FireSalts : InventoryIngredient, IItemEffect
{
	public override string ItemName => "Fire salts";
	public override string IngredientDescription => "Burning remnants of magical flame.";
	public override string EffectDescription => $"Low Fire damage, chance of {Condition.Name}.";

	public List<Damage> Damage => new List<Damage> { new Damage(2, DamageType.Fire) };
	public Condition Condition => new Burning(2, 3);
}