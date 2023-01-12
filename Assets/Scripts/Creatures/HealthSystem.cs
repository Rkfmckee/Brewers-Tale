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

	private void Damage(float damage, DamageType type)
	{
		var actualDamage = CalculateModifiers(damage, type);
		if (actualDamage == 0)
		{
			NotificationManager.AddHealthPopup("No damage", NotificationType.Error, healthBar);
			return;
		}

		var newHealth = CurrentHealth - actualDamage;
		if (newHealth <= 0)
		{
			NotificationManager.AddHealthPopup("Dead", NotificationType.Error, healthBar);
			Destroy(gameObject);
			return;
		}

		CurrentHealth = newHealth;
		NotificationManager.AddHealthPopup($"{actualDamage} {type}", NotificationType.Error, healthBar);
	}

	public void Heal(float health)
	{
		var newHealth = CurrentHealth + health;
		if (newHealth >= MaxHealth)
		{
			NotificationManager.AddHealthPopup("Full health", NotificationType.Success, healthBar);
			CurrentHealth = MaxHealth;
			return;
		}

		CurrentHealth = newHealth;
		NotificationManager.AddHealthPopup(health, NotificationType.Success, healthBar);
	}

	public void CheckForDamagingConditions()
	{
		var damagingConditions = creature.Conditions.OfType<DamagingCondition>();
		if (damagingConditions == null) return;

		foreach (var condition in damagingConditions)
		{
			Damage(condition.Damage);
		}
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
