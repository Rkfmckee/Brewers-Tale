public abstract class InventoryIngredient : InventoryItem
{
	#region Properties

	public override string ItemDescription => string.IsNullOrEmpty(EffectDescription) ? IngredientDescription : $"{IngredientDescription}\n{EffectDescription}";
	public abstract string IngredientDescription { get; }
	public string EffectDescription => GetEffectDescription();

	#endregion
}
