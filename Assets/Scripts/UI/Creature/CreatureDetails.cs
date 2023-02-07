using TMPro;
using UnityEngine;

public class CreatureDetails : MonoBehaviour
{
	#region Constants

	private const float X_POSITION_OFFSET = 0.5f;
	private const float Y_POSITION_OFFSET = 1;
	private const float CHILD_SPACING_PIXELS = 5;

	#endregion

	#region Fields

	[SerializeField]
	private GameObject conditionDetailsPrefab;

	private Creature targetCreature;
	private float childHeightPixels;

	private RectTransform rectTransform;
	private new Camera camera;
	private Transform conditionGroup;

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
