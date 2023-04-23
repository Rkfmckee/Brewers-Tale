public class ThornedVines : InventoryIngredient, IItemEffect
{
	public override string IngredientName => "Thorned vines";
	public override string AestheticDescription => $"Those vines have thorns.";
	public override string EffectDescription => $"Cause {Condition.Name}";

	public Damage Damage => null;
	public Condition Condition => new ThornWrapped(1, 4);
}