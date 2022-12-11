using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brewer : MonoBehaviour
{
	#region Fields

	private BrewerAnimation currentAnimation;
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

	public BrewerAnimation CurrentAnimation
	{
		get => currentAnimation;
		set
		{
			animator.SetTrigger(value.GetDescription());
			animator.applyRootMotion = value == BrewerAnimation.Brew;

			currentAnimation = value;
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

		currentAnimation = BrewerAnimation.Brew;
		animationLengths = new Dictionary<string, float>();
		towardsDeskRotation = transform.rotation;
		awayFromDeskRotation = towardsDeskRotation * Quaternion.Euler(new Vector3(0, 180, 0));

		modelPosition = model.localPosition;
		modelRotation = model.localRotation;
		throwPosition = new Vector3(-2.5f, 1.25f, 0);

		UpdateAnimationLengths();
	}

	#endregion

	#region Methods

	public void TurnAndThrow(PotionType potionType)
	{
		StartCoroutine(TurningAndThrowing(potionType));
	}

	private void UpdateAnimationLengths()
	{
		AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
		foreach (AnimationClip clip in clips)
		{
			animationLengths[clip.name] = clip.length;
		}
	}

	#endregion

	#region Coroutines

	private IEnumerator TurningAndThrowing(PotionType potionType)
	{
		var turnLeftTime = animationLengths[BrewerAnimation.LeftTurn.GetDescription()];
		var turnRightTime = animationLengths[BrewerAnimation.RightTurn.GetDescription()];

		model.localPosition = modelPosition;
		model.localRotation = modelRotation;

		CurrentAnimation = BrewerAnimation.LeftTurn;
		yield return TurnAround(turnLeftTime, towardsDeskRotation, awayFromDeskRotation);

		CurrentAnimation = BrewerAnimation.Throw;
		yield return Throw(potionType);

		CurrentAnimation = BrewerAnimation.RightTurn;
		yield return TurnAround(turnRightTime, awayFromDeskRotation, towardsDeskRotation);

		CurrentAnimation = BrewerAnimation.Brew;
		References.TurnOrderManager.CurrentTurn = new EnemyTurn();
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
		var timer = 0f;
		var throwTime = animationLengths[BrewerAnimation.Throw.GetDescription()];
		var shouldThrowPotion = true;

		while (timer < throwTime)
		{
			if (shouldThrowPotion && timer > throwTime / 3)
			{
				var potion = Instantiate(potionPrefab, throwPosition, Quaternion.identity);
				potion.GetComponent<Potion>().PotionType = potionType;

				shouldThrowPotion = false;
			}

			timer += Time.deltaTime;
			yield return null;
		}
	}

	#endregion
}

