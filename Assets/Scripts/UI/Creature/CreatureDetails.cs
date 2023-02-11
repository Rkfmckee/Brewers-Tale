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

	private Creature targetCreature;
	private float childHeightPixels;

	private RectTransform rectTransform;
	private Transform conditionGroup;
	private new Camera camera;
	private Camera creaturePreviewCamera;

	#endregion

	#region Events

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;

		childHeightPixels = transform.Find("CreatureName").GetComponent<RectTransform>().sizeDelta.y;
		conditionGroup = transform.Find("Conditions");
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

		var creatureName = creature is Enemy ? (creature as Enemy).EnemyName : creature.name;
		transform.Find("CreatureName").Find("Name").GetComponent<TextMeshProUGUI>().SetText(creatureName);

		AddConditionDetails();
		CreateCreaturePreview();
		ShowOtherCreatures(false);
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