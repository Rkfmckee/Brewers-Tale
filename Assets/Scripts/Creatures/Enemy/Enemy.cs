using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static HelperExtensions;

public abstract class Enemy : Creature
{
	#region Fields

	[SerializeField]
	private string enemyName;
	[SerializeField]
	protected int attackDamageAmount;
	[SerializeField]
	protected DamageType attackDamageType;
	[SerializeField]
	private Loot[] lootTable;

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

	public string EnemyName => enemyName;

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

	protected override void Awake()
	{
		base.Awake();

		References.Enemies.Add(this);

		animator = GetComponentInChildren<Animator>();
		animationLengths = GetAnimatorClipLengths(animator);
		animator.speed = animationSpeed.HasValue ? animationSpeed.Value : 1;

		movementTime = 1;
		CurrentState = EnemyState.Idle;

		SetupInherentProperties();
	}

	private void Start()
	{
		brewerHealth = References.Brewer.GetComponent<HealthSystem>();

		enemySpaces = References.EnemySpaces;
		CurrentSpace = enemySpaces[0];
	}

	private void OnDestroy()
	{
		DropLoot();

		if (References.Enemies.Contains(this))
			References.Enemies.Remove(this);
	}

	#endregion

	#region Methods

	protected virtual void SetupInherentProperties()
	{
		foreach (var condition in Conditions)
		{
			condition.IsInherent = true;
		}
	}

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

	private void Attack()
	{
		StartCoroutine(Attacking());
	}

	private void MoveToNextSpace()
	{
		if (CurrentSpace.SpaceNumber == 6) return;

		var nextSpace = enemySpaces[CurrentSpace.SpaceNumber + 1];
		MoveToSpace(nextSpace);
	}

	private void MoveToSpace(EnemySpace space)
	{
		CurrentSpace = space;
		StartCoroutine(MovingToSpace(CurrentSpace));
	}

	private void DropLoot()
	{
		foreach (var loot in lootTable)
		{
			var percentage = Random.Range(0f, 100f);
			if (percentage > loot.DropPercentage) continue;

			var numberToDrop = Random.Range(1, loot.MaxQuantity + 1);
			var numberDropped = 0;
			for (int i = 0; i < numberToDrop; i++)
			{
				var emptySlot = FindEmptyInventorySlot();
				if (!emptySlot) break;

				emptySlot.ItemInSlot = loot.Item;
				numberDropped++;
			}

			switch (numberDropped)
			{
				case 0:
					return;
				case 1:
					NotificationManager.Add($"Picked up {loot.Item.ItemName} from {EnemyName}", NotificationType.Success);
					break;
				default:
					NotificationManager.Add($"Picked up {numberDropped}x {loot.Item.ItemName} from {EnemyName}", NotificationType.Success);
					break;
			}
		}
	}

	private InventorySlot FindEmptyInventorySlot()
	{
		foreach (var slot in References.Inventory.Slots)
		{
			if (!slot.ItemInSlot) return slot;
		}

		return null;
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
