using System.Collections.Generic;

public interface IItemEffect
{
	public Damage Damage { get; }
	public Condition Condition { get; }
}