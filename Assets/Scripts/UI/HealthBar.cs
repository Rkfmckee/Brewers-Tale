
using UnityEngine;

public class HealthBar : MonoBehaviour
{
	#region Fields

	private Transform health;

	private new Camera camera;

	#endregion

	#region Properties

	public HealthSystem Character { get; set; }

	#endregion

	#region Events

	private void Awake()
	{
		camera = Camera.main;
		health = transform.Find("Health");
	}

	private void Start()
	{
		name = $"{Character.name}HealthBar";
	}

	private void LateUpdate()
	{
		// Set position above character's head
		transform.position = camera.WorldToScreenPoint(Character.transform.position + new Vector3(0, Character.HealthBarHeight, 0));
	}

	#endregion

	#region Methods

	public void UpdateHealth(float healthFraction)
	{
		health.localScale = new Vector3(healthFraction, 1, 1);
	}

	#endregion
}