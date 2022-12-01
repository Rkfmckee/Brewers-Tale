public abstract class Turn
{
	#region Properties

	public abstract string TurnText { get; }

	#endregion

	#region Events

	protected abstract void Awake();
	protected abstract void Start();
	protected abstract void Update();


	#endregion
}