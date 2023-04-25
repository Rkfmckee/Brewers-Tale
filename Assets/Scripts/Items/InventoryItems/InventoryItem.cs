using UnityEngine;

public abstract class InventoryItem : MonoBehaviour
{
	#region Fields

	private Slot slotInInventory;

	#endregion

	#region Properties

	public abstract string ItemName { get; }
	public abstract string ItemDescription { get; }

	public Slot SlotInInventory { get => slotInInventory; set => slotInInventory = value; }

	#endregion

	#region Methods

	protected string GetEffectDescription()
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
