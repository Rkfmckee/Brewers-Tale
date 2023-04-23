using System.Collections.Generic;

public interface IItemEffect
{
	public List<Damage> Damage { get; }
	public Condition Condition { get; }
}