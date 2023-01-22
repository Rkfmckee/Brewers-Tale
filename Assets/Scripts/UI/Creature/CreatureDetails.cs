using TMPro;
using UnityEngine;

public class CreatureDetails : MonoBehaviour
{
	#region Constants

	public const float X_POSITION_OFFSET = 0.5f;
	public const float Y_POSITION_OFFSET = 1;

	#endregion

	#region Fields

	private Creature creature;

	private RectTransform rectTransform;
	private new Camera camera;

	#endregion

	#region Events

	private void Awake()
	{
		rectTransform = GetComponent<RectTransform>();
		camera = Camera.main;
	}

	#endregion

	#region Properties

	public void UpdatePosition(Creature creature)
	{
		var creaturePosition = creature.transform.position;
		var halfScreenWidth = Screen.width / 2;
		var creatureOnRight = Mathf.Ceil(camera.WorldToScreenPoint(creaturePosition).x / halfScreenWidth) == 2;
		var positionOffset = new Vector3(X_POSITION_OFFSET, Y_POSITION_OFFSET);

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
	}

	#endregion
}
