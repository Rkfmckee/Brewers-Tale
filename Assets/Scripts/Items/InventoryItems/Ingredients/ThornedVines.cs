using System.Collections.Generic;

public class ThornedVines : InventoryIngredient, IItemEffect
{
	public override string ItemName => "Thorned vines";
	public override string IngredientDescription => $"Those vines have thorns.";

	public List<Damage> Damage => null;
	public Condition Condition => new ThornWrapped(1, 4);
}