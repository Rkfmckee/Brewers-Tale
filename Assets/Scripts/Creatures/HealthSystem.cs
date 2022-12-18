using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float heightOffset;

	private float currentHealth;
	private HealthBar healthBar;

	#endregion

	#region Properties

	public float MaxHealth { get => maxHealth; set => maxHealth = value; }
	public float HeightOffset { get => heightOffset; }

	public float CurrentHealth
	{
		get => currentHealth;
		set
		{
			currentHealth = value;
			healthBar.UpdateHealth(currentHealth / maxHealth);
		}
	}

	#endregion

	#region Events

	private void Start()
	{
		var healthBarPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
		var healthBarGroup = References.UI.Canvas.transform.Find("HealthBars");
		healthBar = Instantiate(healthBarPrefab, healthBarGroup).GetComponent<HealthBar>();
		healthBar.Character = this;

		CurrentHealth = maxHealth;
	}

	private void OnDestroy()
	{
		Destroy(healthBar.gameObject);
	}

	#endregion

	#region Methods

	public void Damage(float damage)
	{
		var newHealth = CurrentHealth - damage;
		if (newHealth <= 0)
		{
			Destroy(gameObject);
			return;
		}

		CurrentHealth = newHealth;
	}

	public void Heal(float health)
	{
		var newHealth = CurrentHealth + health;
		if (newHealth >= MaxHealth)
		{
			CurrentHealth = MaxHealth;
			return;
		}

		CurrentHealth = newHealth;
	}

	#endregion
}
