using UnityEngine;

public abstract class InventoryPotion : InventoryItem, IItemEffect
{
	#region Properties

	public virtual string PotionName { get; }
	public virtual string PotionDescription { get; }
	public virtual Color PotionColour { get; }
	public virtual int EnergyCost { get; }
	public virtual GameObject WorldPrefab { get; }

	// IItemEffect
	public virtual Damage Damage { get; }
	public virtual Condition Condition { get; }

	#endregion

	#region Methods

	public abstract void AffectTarget(Creature target);

	#endregion
}