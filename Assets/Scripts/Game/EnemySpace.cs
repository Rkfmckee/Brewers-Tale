using System.Linq;
using UnityEngine;

public class EnemySpace : MonoBehaviour
{
	#region Fields

	[Range(0, 6)]
	public int SpaceNumber;

	#endregion

	#region Properties

	public Enemy EnemyInSpace { get; set; }

	#endregion

	#region Events

	private void Awake()
	{
		References.EnemySpaces.Add(this);

		OrderSpacesIfLastAdded();
	}

	#endregion

	#region Methods

	private void OrderSpacesIfLastAdded()
	{
		// If this space is the last to be added to the list
		// Order the list by SpaceNumber

		var currentSpaces = References.EnemySpaces;
		if (currentSpaces.Count >= 7)
		{
			References.EnemySpaces = currentSpaces.OrderBy(e => e.SpaceNumber).ToList();
		}
	}

	#endregion
}
