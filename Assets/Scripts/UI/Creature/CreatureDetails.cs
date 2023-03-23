using TMPro;
using UnityEngine;

public class CreatureDetails : MonoBehaviour
{
	#region Constants

	private const float X_POSITION_OFFSET = 0.5f;
	private const float Y_POSITION_OFFSET = 1;
	private const float CHILD_SPACING_PIXELS = 5;
	private const float CREATURE_PREVIEW_CAMERA_Z_OFFSET = -1.5f;
	private const float CREATURE_PREVIEW_CAMERA_Y_DEFAULT = 0.5f;

	#endregion

	#region Fields

	[SerializeField] private GameObject conditionDetailsPrefab;
	[SerializeField] private GameObject creaturePreviewCameraPrefab;
	[SerializeField] private Transform creatureTitleBar;

	private Creature targetCreature;

	private RectTransform rectTransform;
	private Transform conditionGroup;
	private new Camera camera;
	private Camera creaturePreviewCamera;

	private HealthSystem healthSystem;
	private Transform healthBar;
	private TextMeshProUGUI currentHealth;
	private TextMeshProUGUI maxHealth;

	#endregion

	#region Events

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;

		conditionGroup = transform.Find("Conditions");
	}

	private void Update()
	{
		SetCreatureHealth();
	}

	private void OnDestroy()
	{
		Destroy(creaturePreviewCamera.gameObject);
		ShowOtherCreatures(true);
	}

	#endregion

	#region Properties

	public void Initialize(Creature creature)
	{
		targetCreature = creature;

		healthSystem = targetCreature.GetComponent<HealthSystem>();
		healthBar = transform.Find("HealthBar").Find("Health");
		currentHealth = transform.Find("HealthBar").Find("HealthText").Find("CurrentHealth").GetComponent<TextMeshProUGUI>();
		maxHealth = transform.Find("HealthBar").Find("HealthText").Find("MaxHealth").GetComponent<TextMeshProUGUI>();

		var creatureName = creature is Enemy ? (creature as Enemy).EnemyName : creature.name;
		WorldCanvasManager.BookCanvasRight.transform.Find("Pages").Find("Creature").Find("Titlebar").Find("Title").GetComponent<TextMeshProUGUI>().SetText(creatureName);

		AddConditionDetails();
		CreateCreaturePreview();
		ShowOtherCreatures(false);
	}

	private void SetCreatureHealth()
	{
		healthBar.localScale = new Vector3(healthSystem.CurrentHealth / healthSystem.MaxHealth, 1, 1);
		currentHealth.text = healthSystem.CurrentHealth.ToString();
		maxHealth.text = healthSystem.MaxHealth.ToString();
	}

	private void AddConditionDetails()
	{
		foreach (var condition in targetCreature.Conditions)
		{
			var conditionDetails = Instantiate(conditionDetailsPrefab, conditionGroup);
			conditionDetails.transform.Find("Name").GetComponent<TextMeshProUGUI>().SetText(condition.Name);
			conditionDetails.transform.Find("Description").GetComponent<TextMeshProUGUI>().SetText(condition.Description);
		}
	}

	private void CreateCreaturePreview()
	{
		var creatureCollider = targetCreature.GetComponentInChildren<CapsuleCollider>();
		var spawnHeight = creatureCollider ? creatureCollider.height / 2 : CREATURE_PREVIEW_CAMERA_Y_DEFAULT;
		var spawnPosition = targetCreature.transform.position + new Vector3(0, spawnHeight, CREATURE_PREVIEW_CAMERA_Z_OFFSET);
		creaturePreviewCamera = Instantiate(creaturePreviewCameraPrefab, spawnPosition, Quaternion.identity).GetComponent<Camera>();
	}

	private void ShowOtherCreatures(bool shouldShow)
	{
		var creatures = References.Creatures;

		foreach (var creature in creatures)
		{
			if (targetCreature == creature) continue;
			if (!creature.enabled) continue;

			var material = shouldShow ? creature.SolidMaterial : creature.TransparentMaterial;
			var renderers = creature.GetComponentsInChildren<Renderer>();

			foreach (var renderer in renderers)
			{
				renderer.material = material;
			}
		}
	}

	#endregion
}
