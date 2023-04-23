public abstract class InventoryIngredient : InventoryItem
{
	#region Properties

	public override string ItemDescription => string.IsNullOrEmpty(EffectDescription) ? IngredientDescription : $"{IngredientDescription}\n{EffectDescription}";
	public abstract string IngredientDescription { get; }
	public string EffectDescription => GetEffectDescription();

	#endregion

	#region Methods

	private string GetEffectDescription()
	{
		if (this is not IItemEffect) return null;
		var effects = this as IItemEffect;
		var description = "";

		if (effects.Damage != null)
		{
			description += "Damage: ";
			foreach (var damage in effects.Damage)
			{
				description += $"{damage.Amount} {damage.Type}, ";
			}

			description = description.Trim();
			description = description.Trim(',');
			description += "\n";
		}

		if (effects.Condition != null)
		{
			description += $"Condition: {effects.Condition.Name}";
		}

		description = description.Trim();
		return description;
	}

	#endregion
}
