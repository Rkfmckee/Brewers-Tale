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
			UpdateInventoryIcon();
		}
	}

	#endregion

	#region Events

	private void Awake() {
		itemInSlotIcon = transform.Find("ItemIcon").GetComponent<Image>();
		
		UpdateInventoryIcon();
	}

	#endregion

	#region Methods

	private void UpdateInventoryIcon() {
		itemInSlotIcon.sprite = itemInSlot ? itemInSlot.InventoryIcon : null;
		itemInSlotIcon.color  = itemInSlot ? Color.white : Color.clear;
	}

	#endregion
}
