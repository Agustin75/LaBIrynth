﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Inventory")]
public class InventoryManager : ScriptableObject
{
	[SerializeField]
	private List<Item> itemsList;

	// TODO: Remove [SerializeField]. This is for Debug only
	[SerializeField]
	// Inventory should start marked as "Changed" so it displays the items the first time the inventory menu is opened
	private bool inventoryChanged = true;

	// Adds the passed in item to the player's inventory
	// TODO: Will need to change if the player can have multiple copies of the same item
	public void AddItem(Item _item)
	{
		if (itemsList.Contains(_item))
			return;

		itemsList.Add(_item);

		itemsList.Sort();

		inventoryChanged = true;
	}

	// Removes the item passed in from the player's inventory
	public void RemoveItem(Item _item)
	{
		if (itemsList.Contains(_item))
			itemsList.Remove(_item);

		inventoryChanged = true;
	}

	// Returns whether the item exists in the player's inventory
	public bool ContainsItem(Item _item)
	{
		return itemsList.Contains(_item);
	}

	// The Inventory menu was opened
	public void InventoryDisplayed()
	{
		// No change since the last time the Inventory was opened
		inventoryChanged = false;
	}

	// Returns whether an item has been added or removed from the Inventory since the last time it was opened
	public bool HasInventoryChanged()
	{
		return inventoryChanged;
	}

	public List<Item> GetItemList()
	{
		return itemsList;
	}
}
