using UnityEngine;

public class CreatureLocator : Singleton<CreatureLocator>
{
	#region Fields

	private int creatureMask;

	private new Camera camera;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		camera = Camera.main;

		creatureMask = 1 << LayerMask.NameToLayer("Creature");
	}

	private void Update()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		Debug.DrawRay(camera.transform.position, ray.direction, Color.yellow);

		if (Physics.Raycast(ray, out hitInfo, int.MaxValue, creatureMask))
		{
			var objectHit = hitInfo.collider.gameObject;
			var creature = objectHit.GetComponent<Creature>();
			if (creature == null) { print($"{objectHit.name} isn't a creature"); return; }

			// TODO:
			// Check if we're showing this creature's details
			// If not, show it
			// If we are, and it's not this creature's, hide the old one
		}
	}

	#endregion
}
