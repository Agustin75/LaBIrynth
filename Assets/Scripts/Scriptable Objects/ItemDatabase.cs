using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Database/Items")]
public class ItemDatabase : ScriptableObject
{
	[SerializeField]
	private List<Item> sortedItemList;

	[SerializeField]
	[HideInInspector]
	// Needs to be saved so the items' IDs always stay the same between Editor sessions
	private List<Item> itemList;

	public Item GetItem(int _id)
	{
		if (itemList == null || _id < 0 || _id >= itemList.Count)
		{
			Debug.Log("ERROR: Item ID not in array!");
			return null;
		}

		return itemList[_id];
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set Items IDs
	// -------------------------
	// Sets the IDs of all the Items that don't yet have one
	// -------------------------
	// _resetIDs - If it's true, it sets the IDs of all the items again
	// -------------------------
	// Note: This should ONLY be called on debug mode, never from the actual build
	// Note: This should NOT change an items ID to one previously used, or it will mess up the save file
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SetItemsIDs(bool _resetIDs = false)
	{
		// If there are no items
		if (sortedItemList.Count == 0)
			// There is nothing to set
			return;

		// If there is no Item List
		if (itemList == null)
			// Create one
			itemList = new List<Item>();

		if (_resetIDs)
			itemList.Clear();

		// Loop through all the sorted Items
		for (int i = 0; i < sortedItemList.Count; i++)
		{
			// If the Item's ID wasn't set
			if (_resetIDs || sortedItemList[i].GetItemID() == -1)
			{
				// Add the Item to the Item List
				itemList.Add(sortedItemList[i]);
				// Set its ID to the last index of the array
				itemList[i].SetItemID(itemList.Count - 1);
			}
		}
	}
}
