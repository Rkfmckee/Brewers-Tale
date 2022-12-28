using System;

[Serializable]
public class Damage
{
	#region Fields

	public int Amount;
	public DamageType Type;

	#endregion

	#region Constructors

	public Damage(int amount, DamageType type)
	{
		Amount = amount;
		Type = type;
	}

	#endregion
}