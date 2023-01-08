using System.Collections.Generic;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
	private readonly List<Condition> conditions = new List<Condition>();

	#region Properties

	public List<Condition> Conditions => conditions;

	#endregion
}
