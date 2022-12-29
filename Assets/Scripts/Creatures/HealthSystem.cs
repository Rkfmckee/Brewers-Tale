using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float healthBarHeight;
	[SerializeField]
	private List<DamageType> damageImmunities;
	[SerializeField]
	private List<DamageType> damageResistances;
	[SerializeField]
	private List<DamageType> damageVulnerabilities;

	private float currentHealth;
	private HealthBar healthBar;

	#endregion

	#region Properties

	public float MaxHealth { get => maxHealth; set => maxHealth = value; }
	public float HealthBarHeight { get => healthBarHeight; }

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
		if (healthBar == null) return;

		Destroy(healthBar.gameObject);
	}

	#endregion

	#region Methods

	public void Damage(Damage damage)
	{
		Damage(damage.Amount, damage.Type);
	}

	public void Damage(Damage[] damageList)
	{
		foreach (var damage in damageList)
		{
			Damage(damage.Amount, damage.Type);
		}
	}

	public void Damage(float damage, DamageType type)
	{
		var actualDamage = CalculateModifiers(damage, type);
		var newHealth = CurrentHealth - actualDamage;

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

	private float CalculateModifiers(float damage, DamageType type)
	{
		if (damageImmunities.Contains(type))
		{
			return 0;
		}

		if (damageResistances.Contains(type))
		{
			return Mathf.Ceil(damage / 2);
		}

		if (damageVulnerabilities.Contains(type))
		{
			return damage * 2;
		}

		return damage;
	}

	#endregion
}
