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

	private Creature targetCreature;
	private float childHeightPixels;

	private RectTransform rectTransform;
	private new Camera camera;

	#endregion

	#region Events

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;

		childHeightPixels = transform.Find("CreatureName").GetComponent<RectTransform>().sizeDelta.y;
	}

	private void OnDestroy()
	{
		ShowOtherCreatures(true);
	}

	#endregion

	#region Properties

	public void UpdatePosition(Creature creature)
	{
		targetCreature = creature;
		var creaturePosition = creature.transform.position;
		var halfScreenWidth = Screen.width / 2;
		var creatureOnRight = Mathf.Ceil(camera.WorldToScreenPoint(creaturePosition).x / halfScreenWidth) == 2;
		var positionOffset = new Vector3(X_POSITION_OFFSET, Y_POSITION_OFFSET, 0);

		if (creatureOnRight)
		{
			positionOffset.x *= -1;

			foreach (RectTransform child in transform)
			{
				child.pivot = new Vector2(1, child.pivot.y);

				var conditionName = child.Find("Name");
				var conditionDescription = child.Find("Description");
				if (conditionName == null || conditionDescription == null) continue;

				conditionName.GetComponent<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Right;
				conditionDescription.GetComponent<TextMeshProUGUI>().horizontalAlignment = HorizontalAlignmentOptions.Right;
			}
		}

		transform.position += positionOffset;
		var resetZPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
		transform.localPosition = resetZPosition;

		UpdatePositionsOfChildren();
		ShowOtherCreatures(false);
	}

	private void UpdatePositionsOfChildren()
	{
		var numChildren = transform.childCount;
		if (numChildren <= 1) return;

		var yPositionOffset = childHeightPixels + CHILD_SPACING_PIXELS;
		var currentYPosition = (yPositionOffset * (numChildren - 1)) - (yPositionOffset / 2);

		for (int i = 0; i < numChildren; i++)
		{
			var child = transform.GetChild(i);
			child.localPosition = new Vector3(0, currentYPosition, 0);
			currentYPosition -= yPositionOffset;
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
