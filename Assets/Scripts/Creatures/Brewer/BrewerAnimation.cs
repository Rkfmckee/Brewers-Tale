using System.ComponentModel;

public enum BrewerState
{
	[Description("Left Turn")]
	TurningLeft,
	[Description("Right Turn")]
	TurningRight,
	[Description("Throw")]
	Throwing,
	[Description("Bartending")]
	Brewing
}