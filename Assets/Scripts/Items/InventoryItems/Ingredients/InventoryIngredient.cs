public abstract class InventoryIngredient : InventoryItem
{
	#region Properties

	public abstract string IngredientName { get; }
	public string IngredientDescription => string.IsNullOrEmpty(EffectDescription) ? AestheticDescription : $"{AestheticDescription}\nEffects: {EffectDescription}";
	public abstract string AestheticDescription { get; }
	public virtual string EffectDescription { get; }
	public virtual Damage Damage { get; }
	public virtual Condition Condition { get; }

	#endregion
}
