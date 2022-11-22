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
	private Vector3 modelPosition;
	private Quaternion modelRotation;

	private Animator animator;
	private Transform model;

	#endregion

	#region Properties

	private BrewerAnimation CurrentAnimation
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
		animator = GetComponentInChildren<Animator>();
		model = transform.Find("Model");

		currentAnimation = BrewerAnimation.Brew;
		animationLengths = new Dictionary<string, float>();
		towardsDeskRotation = transform.rotation;
		awayFromDeskRotation = towardsDeskRotation * Quaternion.Euler(new Vector3(0, 180, 0));

		modelPosition = model.localPosition;
		modelRotation = model.localRotation;

		UpdateAnimationLengths();
	}

	private void Update()
	{
	}

	#endregion

	#region Methods

	public void UpdateAnimationLengths()
	{
		AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
		foreach (AnimationClip clip in clips)
		{
			animationLengths[clip.name] = clip.length;
		}
	}

	#endregion

	#region Coroutines

	private IEnumerator TurnAndThrow()
	{
		var turnLeftTime = animationLengths[BrewerAnimation.LeftTurn.GetDescription()];
		var throwTime = animationLengths[BrewerAnimation.Throw.GetDescription()];
		var turnRightTime = animationLengths[BrewerAnimation.RightTurn.GetDescription()];

		model.localPosition = modelPosition;
		model.localRotation = modelRotation;

		CurrentAnimation = BrewerAnimation.LeftTurn;
		yield return TimeAndMove(turnLeftTime, towardsDeskRotation, awayFromDeskRotation);

		CurrentAnimation = BrewerAnimation.Throw;
		yield return TimeAndMove(throwTime);

		CurrentAnimation = BrewerAnimation.RightTurn;
		yield return TimeAndMove(turnRightTime, awayFromDeskRotation, towardsDeskRotation);

		CurrentAnimation = BrewerAnimation.Brew;
	}

	private IEnumerator TimeAndMove(float totalTime, Quaternion? rotationFrom = null, Quaternion? rotationTo = null)
	{
		var timer = 0f;

		while (timer < totalTime)
		{
			if (rotationFrom.HasValue && rotationTo.HasValue)
				transform.rotation = Quaternion.Lerp(rotationFrom.Value, rotationTo.Value, timer / totalTime);

			timer += Time.deltaTime;
			yield return null;
		}

		if (rotationTo.HasValue) transform.rotation = rotationTo.Value;
	}

	#endregion
}

