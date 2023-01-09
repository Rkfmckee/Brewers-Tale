using System.Linq;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private float maxHealth;
	[SerializeField]
	private float healthBarHeight;

	private float currentHealth;
	private HealthBar healthBar;

	private Creature creature;

	#endregion

	#region Properties

	public float MaxHealth { get => maxHealth; set => maxHealth = value; }
	public float HealthBarHeight => healthBarHeight;
	public HealthBar HealthBar => healthBar;

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

	private void Awake()
	{
		creature = GetComponent<Creature>();

		var healthBarPrefab = Resources.Load<GameObject>("Prefabs/UI/Health/HealthBar");
		var healthBarGroup = WorldCanvasManager.Canvas.transform.Find("HealthBars");
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
		if (actualDamage == 0)
		{
			HealthPopup.Create(healthBar, "No damage", true);
			return;
		}

		var newHealth = CurrentHealth - actualDamage;
		if (newHealth <= 0)
		{
			HealthPopup.Create(healthBar, "Dead", true);
			Destroy(gameObject);
			return;
		}

		CurrentHealth = newHealth;
		HealthPopup.Create(healthBar, actualDamage, true);
	}

	public void Heal(float health)
	{
		var newHealth = CurrentHealth + health;
		if (newHealth >= MaxHealth)
		{
			HealthPopup.Create(healthBar, "Full health", false);
			CurrentHealth = MaxHealth;
			return;
		}

		CurrentHealth = newHealth;
		HealthPopup.Create(healthBar, health, false);
	}

	private float CalculateModifiers(float damage, DamageType type)
	{
		damage = CalculateImmunities(damage, type);
		damage = CalculateResistances(damage, type);
		damage = CalculateVulnerabilities(damage, type);

		return damage;
	}

	private float CalculateImmunities(float damage, DamageType type)
	{
		var damageImmunities = creature.Conditions.Where(c => c is DamageImmunity);
		if (damageImmunities == null) return damage;

		if (damageImmunities.Any(di => (di as DamageImmunity).ImmuneType == type)) return 0;

		return damage;
	}

	private float CalculateResistances(float damage, DamageType type)
	{
		var damageResistances = creature.Conditions.Where(c => c is DamageResistance);
		if (damageResistances == null) return damage;

		if (damageResistances.Any(dr => (dr as DamageResistance).ResistedType == type)) return Mathf.Floor(damage / 2);

		return damage;
	}

	private float CalculateVulnerabilities(float damage, DamageType type)
	{
		var damageVulnerabilities = creature.Conditions.Where(c => c is DamageVulnerability);
		if (damageVulnerabilities == null) return damage;

		if (damageVulnerabilities.Any(dv => (dv as DamageVulnerability).VulnerableType == type)) return damage * 2;

		return damage;
	}

	#endregion
}
