using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HelperExtensions;

public class Brewer : MonoBehaviour
{
	#region Fields

	private BrewerState currentState;
	private Dictionary<string, float> animationLengths;
	private Quaternion towardsDeskRotation;
	private Quaternion awayFromDeskRotation;
	private Quaternion modelRotation;
	private Vector3 modelPosition;
	private Vector3 throwPosition;

	private Animator animator;
	private Transform model;
	private GameObject potionPrefab;

	#endregion

	#region Properties

	public BrewerState CurrentState
	{
		get => currentState;
		set
		{
			animator.SetTrigger(value.GetDescription());
			animator.applyRootMotion = value == BrewerState.Brewing;

			currentState = value;
		}
	}

	#endregion

	#region Events

	private void Awake()
	{
		References.Brewer = this;

		animator = GetComponentInChildren<Animator>();
		model = transform.Find("Model");
		potionPrefab = Resources.Load<GameObject>($"Prefabs/Items/Potions/Potion");

		currentState = BrewerState.Brewing;
		animationLengths = GetAnimatorClipLengths(animator);
		towardsDeskRotation = transform.rotation;
		awayFromDeskRotation = towardsDeskRotation * Quaternion.Euler(new Vector3(0, 180, 0));

		modelPosition = model.localPosition;
		modelRotation = model.localRotation;
		throwPosition = new Vector3(-2.5f, 1.25f, 0);
	}

	#endregion

	#region Methods

	public void TurnAndThrow(PotionType potionType)
	{
		StartCoroutine(TurningAndThrowing(potionType));
	}

	private Vector3? GetPotionTargetPosition()
	{
		foreach (var space in References.EnemySpaces.OrderByDescending(e => e.SpaceNumber))
		{
			if (space.EnemyInSpace != null) return space.transform.position;
		}

		return null;
	}

	#endregion

	#region Coroutines

	private IEnumerator TurningAndThrowing(PotionType potionType)
	{
		var turnLeftTime = animationLengths[BrewerState.TurningLeft.GetDescription()];
		var turnRightTime = animationLengths[BrewerState.TurningRight.GetDescription()];

		model.localPosition = modelPosition;
		model.localRotation = modelRotation;

		CurrentState = BrewerState.TurningLeft;
		yield return TurnAround(turnLeftTime, towardsDeskRotation, awayFromDeskRotation);

		CurrentState = BrewerState.Throwing;
		yield return Throw(potionType);

		CurrentState = BrewerState.TurningRight;
		yield return TurnAround(turnRightTime, awayFromDeskRotation, towardsDeskRotation);

		CurrentState = BrewerState.Brewing;
	}

	private IEnumerator TurnAround(float totalTime, Quaternion rotationFrom, Quaternion rotationTo)
	{
		var timer = 0f;

		while (timer < totalTime)
		{
			transform.rotation = Quaternion.Lerp(rotationFrom, rotationTo, timer / totalTime);

			timer += Time.deltaTime;
			yield return null;
		}

		transform.rotation = rotationTo;
	}

	private IEnumerator Throw(PotionType potionType)
	{
		var potionTargetPosition = GetPotionTargetPosition();
		if (!potionTargetPosition.HasValue)
		{
			print("No enemies to throw at");
			yield break;
		}

		var timer = 0f;
		var throwTime = animationLengths[BrewerState.Throwing.GetDescription()];
		var shouldThrowPotion = true;

		while (timer < throwTime)
		{
			if (shouldThrowPotion && timer > throwTime / 3)
			{
				var potionObject = Instantiate(potionPrefab, throwPosition, Quaternion.identity);
				var potion = potionObject.GetComponent<Potion>();
				potion.PotionType = potionType;
				potion.TargetPosition = potionTargetPosition;

				shouldThrowPotion = false;
			}

			timer += Time.deltaTime;
			yield return null;
		}
	}

	#endregion
}

