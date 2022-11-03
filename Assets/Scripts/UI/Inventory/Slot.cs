using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private InventoryItem itemInSlot;

	#endregion

	#region Properties

	public InventoryItem ItemInSlot
	{
		get => itemInSlot;
		set
		{
			itemInSlot = value;

			InstantiateItemInSlot();
			UpdateInventorySlot();
		}
	}

	#endregion

	#region Events

	protected virtual void Awake()
	{
	}

	private void Start()
	{
		InstantiateItemInSlot();
		UpdateInventorySlot();
	}

	#endregion

	#region Methods

	protected virtual void UpdateInventorySlot()
	{
		if (!itemInSlot) return;
		
		itemInSlot.SlotInInventory = this;
		itemInSlot.transform.localPosition = Vector3.zero;
	}

	private void InstantiateItemInSlot()
	{
		if (!itemInSlot) return;

		var itemScene = itemInSlot.gameObject.scene.name;
		var isPrefab  = itemScene == null || itemScene == itemInSlot.gameObject.name;
		var instanceOfItem = null as GameObject;

		if (!isPrefab) return;

		instanceOfItem      = Instantiate(itemInSlot.gameObject, transform);
		instanceOfItem.name = itemInSlot.name;
		itemInSlot          = instanceOfItem.GetComponent<InventoryItem>();
	}

	#endregion
}