public class FrostFlecks : InventoryIngredient, IItemEffect
{
	public override string IngredientName => "Frost flecks";
	public override string AestheticDescription => $"They'reee greeaattt.";
	public override string EffectDescription => $"Low {Damage.Type} damage";

	public Damage Damage => new Damage(3, DamageType.Cold);
	public Condition Condition => null;
}