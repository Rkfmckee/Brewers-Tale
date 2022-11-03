using UnityEngine;
using UnityEngine.UI;

public abstract class Slot : MonoBehaviour
{
	#region Fields

	[SerializeField]
	private InventoryItem itemInSlot;

	private Image itemInSlotIcon;

	#endregion

	#region Properties

	public InventoryItem ItemInSlot
	{
		get => itemInSlot;
		set
		{
			itemInSlot = value;
			UpdateInventorySlot();
		}
	}

	#endregion

	#region Events

	protected virtual void Awake()
	{
		itemInSlotIcon = transform.Find("ItemIcon").GetComponent<Image>();
	}

	private void Start()
	{
		UpdateInventorySlot();
	}

	#endregion

	#region Methods

	protected virtual void UpdateInventorySlot()
	{
		if (itemInSlot) itemInSlot.SlotInInventory = this;

		itemInSlotIcon.sprite = itemInSlot ? itemInSlot.InventoryIcon : null;
		itemInSlotIcon.color  = itemInSlot ? Color.white : Color.clear;
	}

	#endregion
}