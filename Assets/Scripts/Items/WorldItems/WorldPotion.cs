using UnityEngine;

public class WorldPotion : MonoBehaviour
{
	#region Constants

	private const float HEIGHT_SCALING = -0.25f;
	private const int ROTATION_DEGREES = 5;

	#endregion

	#region Field

	private GameObject splashPrefab;
	private PotionType potionType;
	private Vector3 startPosition;
	private float throwSpeed;
	private float arcHeight;

	private GameObject potionLiquid;

	#endregion

	#region Properties

	public Creature Target { get; set; }

	public InventoryPotion InventoryPotion { get; set; }

	#endregion

	#region Events

	protected virtual void Start()
	{
		potionLiquid = transform.Find("Liquid").gameObject;
		potionLiquid.GetComponent<MeshRenderer>().material.color = InventoryPotion.PotionColour;

		splashPrefab = Resources.Load<GameObject>("Prefabs/Items/WorldItems/PotionSplash");

		startPosition = transform.position;
		throwSpeed = 5;
		arcHeight = 1;
	}

	private void Update()
	{
		if (!Target) return;

		transform.position = CalculatePosition();
		transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -ROTATION_DEGREES);

		if (transform.position == Target.transform.position) Arrived();
	}

	#endregion

	#region Methods

	private Vector3 CalculatePosition()
	{
		// Parabola algorithm from https://luminaryapps.com/blog/arcing-projectiles-in-unity/

		var targetPosition = Target.transform.position;
		var startX = startPosition.x;
		var targetX = targetPosition.x;
		var distance = targetX - startX;

		var nextXPosition = Mathf.MoveTowards(transform.position.x, targetX, throwSpeed * Time.deltaTime);
		var baseYPosition = Mathf.Lerp(startPosition.y, targetPosition.y, (nextXPosition - startX) / distance);
		var arc = arcHeight * (nextXPosition - startX) * (nextXPosition - targetX) / (HEIGHT_SCALING * distance * distance);
		var nextYPosition = baseYPosition + arc;

		return new Vector3(nextXPosition, nextYPosition, transform.position.z);
	}

	private void Arrived()
	{
		var splash = Instantiate(splashPrefab, transform.position, Quaternion.identity);
		var particleSystem = splash.transform.Find("Particles").GetComponent<ParticleSystem>().main;
		particleSystem.startColor = InventoryPotion.PotionColour;

		InventoryPotion.AffectTarget(Target);

		Destroy(splash, particleSystem.duration);
		Destroy(gameObject);
	}

	#endregion
}
