using UnityEngine;

public abstract class Slot : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private InventoryItem itemInSlot;
	protected GameObject itemInstance;

	#endregion

	#region Properties

	public InventoryItem ItemInSlot
	{
		get => itemInSlot;
		set
		{
			itemInSlot = value;
			UpdateInventoryItem();
		}
	}

	#endregion

	#region Events

	protected virtual void Awake()
	{
	}

	private void Start()
	{
		UpdateInventoryItem();
	}

	#endregion

	#region Methods

	#region Abstract methods

	public abstract void ItemPickedUp();

	#endregion

	protected virtual void UpdateInventoryItem()
	{
		if (!itemInSlot)
		{
			itemInstance = null;
			return;
		}

		var itemScene = itemInSlot.gameObject.scene.name;
		var isPrefab = itemScene == null || itemScene == itemInSlot.gameObject.name;

		if (isPrefab)
		{
			itemInstance = Instantiate(itemInSlot.gameObject, transform);
			itemInstance.name = itemInSlot.name;
			itemInSlot = itemInstance.GetComponent<InventoryItem>();
		}

		itemInSlot.SlotInInventory = this;
		itemInSlot.transform.SetParent(transform, false);
		itemInSlot.transform.localPosition = Vector3.zero;
	}

	#endregion
}