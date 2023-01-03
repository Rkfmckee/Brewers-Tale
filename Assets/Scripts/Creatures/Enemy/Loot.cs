using System;
using UnityEngine;

[Serializable]
public class Loot
{
	public InventoryItem Item;
	public float DropPercentage;
	[Range(1, 10)]
	public int MaxQuantity = 1;
}