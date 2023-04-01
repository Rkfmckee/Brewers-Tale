public abstract class TemporaryCondition : Condition
{
	public virtual int NumberOfTurns { get; set; }
	public string TurnOrTurns => NumberOfTurns == 1 ? "turn" : "turns";
	public string NumberOfTurnsText => $"{NumberOfTurns} {TurnOrTurns}";
}