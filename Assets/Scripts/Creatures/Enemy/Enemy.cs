using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HelperExtensions;

public abstract class Enemy : MonoBehaviour
{
	#region Fields

	[SerializeField]
	protected int attackDamageAmount;
	[SerializeField]
	protected DamageType attackDamageType;

	protected float? animationSpeed;
	private EnemyState currentState;
	private EnemySpace currentSpace;
	private float movementTime;
	private List<EnemySpace> enemySpaces;

	private Animator animator;
	private Dictionary<string, float> animationLengths;
	private HealthSystem brewerHealth;

	#endregion

	#region Properties

	public EnemyState CurrentState
	{
		get => currentState;
		set
		{
			animator.ResetTrigger(currentState.GetDescription());
			currentState = value;
			animator.SetTrigger(currentState.GetDescription());
		}
	}
	public EnemySpace CurrentSpace
	{
		get => currentSpace;
		set
		{
			// Remove enemy from previous space
			if (currentSpace != null)
				if (GameObject.ReferenceEquals(currentSpace.EnemyInSpace, this))
					currentSpace.EnemyInSpace = null;

			currentSpace = value;
			currentSpace.EnemyInSpace = this;
		}
	}


	#endregion

	#region Events

	protected virtual void Awake()
	{
		References.Enemies.Add(this);

		animator = GetComponentInChildren<Animator>();
		animationLengths = GetAnimatorClipLengths(animator);
		animator.speed = animationSpeed.HasValue ? animationSpeed.Value : 1;

		movementTime = 1;
		CurrentState = EnemyState.Idle;
	}

	private void Start()
	{
		brewerHealth = References.Brewer.GetComponent<HealthSystem>();

		enemySpaces = References.EnemySpaces;
		CurrentSpace = enemySpaces[0];
	}

	private void OnDestroy()
	{
		if (References.Enemies.Contains(this))
			References.Enemies.Remove(this);
	}

	#endregion

	#region Methods

	public void TakeTurn()
	{
		if (CurrentSpace.SpaceNumber == 6)
		{
			Attack();
			return;
		}

		if (enemySpaces[CurrentSpace.SpaceNumber + 1].EnemyInSpace == null)
		{
			MoveToNextSpace();
			return;
		}
	}

	public void Attack()
	{
		StartCoroutine(Attacking());
	}

	public void MoveToNextSpace()
	{
		if (CurrentSpace.SpaceNumber == 6) return;

		var nextSpace = enemySpaces[CurrentSpace.SpaceNumber + 1];
		MoveToSpace(nextSpace);
	}

	public void MoveToSpace(EnemySpace space)
	{
		CurrentSpace = space;
		StartCoroutine(MovingToSpace(CurrentSpace));
	}

	#endregion

	#region Coroutines

	private IEnumerator Attacking()
	{
		CurrentState = EnemyState.Attacking;

		var timer = 0f;
		var attackTime = animationLengths[CurrentState.GetDescription()] / animationSpeed;
		var shouldCauseDamage = true;

		while (timer < attackTime)
		{
			// Check if we're at the right time in the animation to cause damage
			// And use the boolean to make sure we only do it once
			if (shouldCauseDamage && timer > attackTime / 2)
			{
				// Damage brewer
				brewerHealth.Damage(attackDamageAmount, attackDamageType);
				shouldCauseDamage = false;
			}

			timer += Time.deltaTime;
			yield return null;
		}

		CurrentState = EnemyState.Idle;
	}

	private IEnumerator MovingToSpace(EnemySpace space)
	{
		CurrentState = EnemyState.Moving;

		var startingPosition = enemySpaces[CurrentSpace.SpaceNumber - 1].transform.position;
		var timer = 0f;

		while (Vector3.Distance(transform.position, space.transform.position) > 0)
		{
			transform.position = Vector3.Lerp(startingPosition, space.transform.position, timer / movementTime);
			timer += Time.deltaTime;
			yield return null;
		}

		CurrentState = EnemyState.Idle;
	}

	#endregion
}
