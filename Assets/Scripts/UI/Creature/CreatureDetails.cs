using TMPro;
using UnityEngine;

public class CreatureDetails : MonoBehaviour
{
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
		// If the slot is on the right side of a page,
		// Move the ItemDetails to the left side of the slot.
		// The right side of the page is the 2nd or 4th quarter
		// of the screen.

		var creaturePosition = creature.transform.position;
		var halfScreenWidth = Screen.width / 2;
		var creatureOnRight = Mathf.Ceil(camera.WorldToScreenPoint(creaturePosition).x / halfScreenWidth) == 2;

		if (creatureOnRight)
		{
			rectTransform.localPosition *= -1;

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
	}

	#endregion
}
