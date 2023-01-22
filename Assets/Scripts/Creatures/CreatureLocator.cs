using UnityEngine;

public class CreatureLocator : Singleton<CreatureLocator>
{
	#region Fields

	private int creatureMask;
	private bool showingCreatureDetails;

	private new Camera camera;
	private WorldCanvasManager canvasManager;

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		camera = Camera.main;
		canvasManager = WorldCanvasManager.Instance;

		creatureMask = 1 << LayerMask.NameToLayer("Creature");
		showingCreatureDetails = false;
	}

	private void Update()
	{
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hitInfo;
		//Debug.DrawRay(camera.transform.position, ray.direction, Color.yellow);

		if (Physics.Raycast(ray, out hitInfo, int.MaxValue, creatureMask))
		{
			var objectHit = hitInfo.collider.gameObject;
			var creature = objectHit.GetComponent<Creature>();
			if (creature == null) { print($"{objectHit.name} isn't a creature"); return; }

			if (canvasManager.CurrentCreatureDetails(creature)) return;

			WorldCanvasManager.Instance.ShowCreatureDetails(creature);
			showingCreatureDetails = true;
			return;
		}

		if (showingCreatureDetails) canvasManager.HideCreatureDetails();
	}

	#endregion
}
