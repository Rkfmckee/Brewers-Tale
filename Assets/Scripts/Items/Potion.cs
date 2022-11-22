using UnityEngine;

public class Potion : MonoBehaviour
{
	#region Constants

	private const float HEIGHT_SCALING = -0.25f;
	private const int ROTATION_DEGREES = 5;

	#endregion

	#region Field

	private Vector3 startPosition;
	private Vector3 targetPosition;
	private float throwSpeed;
	private float arcHeight;

	#endregion

	#region Events

	private void Awake()
	{
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
		Destroy(gameObject);
	}

	#endregion
}
