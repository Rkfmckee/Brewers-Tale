using TMPro;
using UnityEngine;

public class WorldCanvasManager : Singleton<WorldCanvasManager>
{
	#region Fields

	private Canvas canvas;
	private Canvas bookCanvasLeft;
	private Canvas bookCanvasRight;

	private Creature currentCreature;
	private CreatureDetails currentCreatureDetails;
	private GameObject creatureDetailsPrefab;

	#endregion

	#region Properties

	public static Canvas Canvas { get => Instance.canvas; }
	public static Canvas BookCanvasLeft { get => Instance.bookCanvasLeft; }
	public static Canvas BookCanvasRight { get => Instance.bookCanvasRight; }

	#endregion

	#region Events

	protected override void Awake()
	{
		base.Awake();

		var book = GameObject.Find("Book").transform;
		canvas = GameObject.Find("WorldCanvas").GetComponent<Canvas>();
		bookCanvasLeft = book.Find("CanvasLeft").GetComponent<Canvas>();
		bookCanvasRight = book.Find("CanvasRight").GetComponent<Canvas>();

		creatureDetailsPrefab = Resources.Load<GameObject>("Prefabs/UI/Creature/CreatureDetails");
	}

	#endregion

	#region Methods

	public bool CurrentCreatureDetails(Creature creature)
	{
		return ReferenceEquals(creature, currentCreature);
	}

	public void ShowCreatureDetails(Creature creature)
	{
		var spawnPosition = creature.transform.position;
		currentCreature = creature;
		currentCreatureDetails = Instantiate(creatureDetailsPrefab, spawnPosition, Quaternion.identity, canvas.transform).GetComponent<CreatureDetails>();
		currentCreatureDetails.Initialize(creature);
	}

	public void HideCreatureDetails()
	{
		if (currentCreatureDetails == null) return;

		currentCreature = null;
		Destroy(currentCreatureDetails.gameObject);
	}

	#endregion
}
