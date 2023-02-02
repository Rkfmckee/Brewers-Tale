using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;

public abstract class Creature : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private Material transparentMaterial;

	private readonly List<Condition> conditions = new List<Condition>();

	private Material solidMaterial;
	protected HealthSystem healthSystem;

	#endregion

	#region Properties

	public Material TransparentMaterial => transparentMaterial;
	public Material SolidMaterial => solidMaterial;
	public ReadOnlyCollection<Condition> Conditions => conditions.AsReadOnly();

	#endregion

	#region Events

	protected virtual void Awake()
	{
		References.Creatures.Add(this);

		solidMaterial = GetComponentInChildren<Renderer>().material;
		healthSystem = GetComponent<HealthSystem>();
		conditions.Add(new Burning(0, 10));
	}

	#endregion

	#region Methods

	public void AddCondition(Condition condition)
	{
		if (conditions.Where(c => c.GetType() == condition.GetType()).Any()) return;

		conditions.Add(condition);
		NotificationManager.AddHealthPopup($"+ {condition.Name}", NotificationType.Success, healthSystem.HealthBar);
	}

	public void RemoveConditions(IEnumerable<ITemporaryCondition> conditions)
	{
		foreach (var condition in conditions)
		{
			RemoveCondition(condition);
		}
	}

	public void RemoveConditions(IEnumerable<Condition> conditions)
	{
		foreach (var condition in conditions)
		{
			RemoveCondition(condition);
		}
	}

	// Remove a specific instance of a temporary condition
	public void RemoveCondition(ITemporaryCondition condition)
	{
		RemoveCondition(condition as Condition);
	}

	// Remove a specific instance of a condition
	public void RemoveCondition(Condition condition)
	{
		if (!conditions.Contains(condition)) return;

		conditions.Remove(condition);
		NotificationManager.AddHealthPopup($"- {condition.Name}", NotificationType.Error, healthSystem.HealthBar);
	}

	// Remove a type of condition
	public void RemoveCondition<T>()
		where T : Condition
	{
		var condition = conditions.OfType<T>().SingleOrDefault();
		if (condition == null) return;

		conditions.Remove(condition);
		NotificationManager.AddHealthPopup($"- {condition.Name}", NotificationType.Error, healthSystem.HealthBar);
	}

	#endregion
}
