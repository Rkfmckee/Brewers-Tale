using System.ComponentModel;

public enum EnemyState
{
	Idle,
	[Description("Move")]
	Moving,
	[Description("Attack")]
	Attacking,
	[Description("Die")]
	Dead
}
