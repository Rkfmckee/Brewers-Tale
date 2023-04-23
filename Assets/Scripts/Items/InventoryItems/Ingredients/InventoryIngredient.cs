public abstract class InventoryIngredient : InventoryItem
{
	#region Properties

	public override string ItemDescription => string.IsNullOrEmpty(EffectDescription) ? IngredientDescription : $"{IngredientDescription}\nEffects: {EffectDescription}";
	public abstract string IngredientDescription { get; }
	public virtual string EffectDescription { get; }

	#endregion
}
