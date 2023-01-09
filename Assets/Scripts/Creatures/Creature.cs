using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
	#region Fields

	private readonly List<Condition> conditions = new List<Condition>();

	protected HealthSystem healthSystem;

	#endregion

	#region Properties

	public ReadOnlyCollection<Condition> Conditions => conditions.AsReadOnly();

	#endregion

	#region Events

	protected virtual void Awake()
	{
		healthSystem = GetComponent<HealthSystem>();
	}

	#endregion

	#region Methods

	public void AddCondition(Condition condition)
	{
		if (conditions.Where(c => c.GetType() == condition.GetType()).Any()) return;

		conditions.Add(condition);
		HealthPopup.Create(healthSystem.HealthBar, $"+ {condition.Name}", false);
	}

	public void RemoveCondition<T>()
		where T : Condition
	{
		var condition = conditions.OfType<T>().SingleOrDefault();
		if (condition == null) return;

		conditions.Remove(condition);
		HealthPopup.Create(healthSystem.HealthBar, $"- {condition.Name}", true);
	}

	#endregion
}
