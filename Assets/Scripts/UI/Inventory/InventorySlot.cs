using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
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

	private void Awake() {
		itemInSlotIcon = transform.Find("ItemIcon").GetComponent<Image>();
		
		UpdateInventorySlot();
	}

	#endregion

	#region Methods

	private void UpdateInventorySlot() {
		if (itemInSlot) itemInSlot.InventorySlot = this;
		
		itemInSlotIcon.sprite = itemInSlot ? itemInSlot.InventoryIcon : null;
		itemInSlotIcon.color  = itemInSlot ? Color.white : Color.clear;
	}

	#endregion
}
