public class ThornedVines : InventoryIngredient
{
	public override string IngredientName => "Thorned vines";
	public override string AestheticDescription => $"Those vines have thorns.";
	public override string EffectDescription => $"Cause {Condition.Name}";
	public override Condition Condition => new ThornWrapped(1, 4);
}