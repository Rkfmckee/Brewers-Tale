using UnityEngine;

public class EnemySpace : MonoBehaviour
{
	#region Fields

	[Range(1, 6)]
	public int SpaceNumber;

	#endregion

	#region Events

	private void Awake()
	{
		References.EnemySpaces.Add(this);
	}

	#endregion
}
