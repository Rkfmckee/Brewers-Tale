using UnityEngine;

public class Potion : MonoBehaviour
{
	#region Constants

	private const float HEIGHT_SCALING = -0.25f;
	private const int ROTATION_DEGREES = 5;

	#endregion

	#region Field

	[SerializeField]
	private GameObject splashPrefab;

	private PotionType potionType;
	private Color potionColour;
	private Vector3 startPosition;
	private Vector3 targetPosition;
	private float throwSpeed;
	private float arcHeight;

	private GameObject potionLiquid;

	#endregion

	#region Properties

	public PotionType PotionType
	{
		get => potionType;
		set
		{
			potionType = value;
			SetPotionColour();
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		potionLiquid = transform.Find("Liquid").gameObject;

		startPosition = transform.position;
		targetPosition = new Vector3(2, 0, 0);
		throwSpeed = 5;
		arcHeight = 1;
	}

	private void Update()
	{
		transform.position = CalculatePosition();
		transform.rotation = transform.rotation * Quaternion.Euler(0, 0, -ROTATION_DEGREES);

		if (transform.position == targetPosition) Arrived();
	}

	#endregion

	#region Methods

	private void SetPotionColour()
	{
		switch (potionType)
		{
			case PotionType.Red:
				potionColour = Color.red;
				break;
			case PotionType.Blue:
				potionColour = Color.blue;
				break;
			case PotionType.Purple:
				potionColour = new Color(0.5f, 0.25f, 1);
				break;
		}

		potionLiquid.GetComponent<MeshRenderer>().material.color = potionColour;
	}

	private Vector3 CalculatePosition()
	{
		// Parabola algorithm from https://luminaryapps.com/blog/arcing-projectiles-in-unity/

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
		particleSystem.startColor = potionColour;

		Destroy(splash, particleSystem.duration);
		Destroy(gameObject);
	}

	#endregion
}
