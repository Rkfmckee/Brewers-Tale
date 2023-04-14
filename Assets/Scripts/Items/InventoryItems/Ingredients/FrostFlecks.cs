public class FrostFlecks : InventoryIngredient
{
	public override string IngredientName => "Frost flecks";
	public override string AestheticDescription => $"They'reee greeaattt.";
	public override string EffectDescription => $"Low {Damage.Type} damage";
	public override Damage Damage => new Damage(3, DamageType.Cold);
}