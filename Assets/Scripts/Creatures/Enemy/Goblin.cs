public class Goblin : Enemy
{
	#region Events

	protected override void Awake()
	{
		animationSpeed = 0.75f;

		base.Awake();
	}

	#endregion
}
