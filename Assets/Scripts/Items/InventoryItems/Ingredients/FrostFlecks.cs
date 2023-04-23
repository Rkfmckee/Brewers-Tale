using System.Collections.Generic;

public class FrostFlecks : InventoryIngredient, IItemEffect
{
	public override string ItemName => "Frost flecks";
	public override string IngredientDescription => $"They'reee greeaattt.";

	public List<Damage> Damage => new List<Damage> { new Damage(3, DamageType.Cold) };
	public Condition Condition => null;
}