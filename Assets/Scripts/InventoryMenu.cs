using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject inventoryMenuParent;

	[SerializeField]
	private Transform itemsParent, upgradesParent;

	[SerializeField]
	private GameObject itemPrefab;

	[SerializeField]
	private Button prevPageButton, nextPageButton;

	[Header("Scriptable Objects")]
	[SerializeField]
	private InventoryManager inventory;
	[SerializeField]
	private UpgradesManager upgradesManager;
	[SerializeField]
	private List<UpgradeInfo> upgradesInfo;

	private List<ItemInfoDisplay> itemsInfoList;
	private List<UpgradeInfoDisplay> upgradesInfoList;
	private int prevNumOfItems, numItemSlots;

	// Variables for looking through pages
	private int numPages, currPage;

    // Start is called before the first frame update
    void Start()
    {
		itemsInfoList = new List<ItemInfoDisplay>(itemsParent.GetComponentsInChildren<ItemInfoDisplay>());
		upgradesInfoList = new List<UpgradeInfoDisplay>(upgradesParent.GetComponentsInChildren<UpgradeInfoDisplay>());

		// TODO: Change to use the saved info
		prevNumOfItems = 0;
		numItemSlots = itemsInfoList.Count;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void OpenInventory()
	{
		if (inventory.HasInventoryChanged())
		{
			// TODO: Display all items currently in the inventory
			List<Item> itemList = inventory.GetItemList();

			// Save the new number of pages (If the player has no items, there are no pages)
			numPages = itemList.Count == 0 ? 0 : ((itemList.Count - 1) / numItemSlots) + 1;

			if (currPage < 0)
			{
				currPage = 0;
			}
			// If the current page would go past the new number of pages
			else if (currPage >= numPages)
			{
				// Set the current page to the last page
				currPage = numPages - 1;
			}

			// If the player has at least 1 item in the inventory
			if (currPage != -1)
			{
				// Check whether the player is now in the last page
				nextPageButton.interactable = currPage < numPages - 1;

				// Check whether the player is now in the first page
				prevPageButton.interactable = currPage > 0;
			}
			// The player has no items, so there are no pages to move through
			else
			{
				nextPageButton.interactable = false;
				prevPageButton.interactable = false;
			}

			DisplayCurrentPage(itemList);

			prevNumOfItems = itemList.Count;

			inventory.InventoryDisplayed();
		}

		if (upgradesManager.WasUpgradeObtained())
		{
			List<BoolVariable> upgradeStatus = upgradesManager.GetUpgradesStatus();

			// Update the upgrades display to show all owned
			//for (int i = 0; i < upgradeStatus.Count; i++)
			//{
			//	upgradesInfoList[i].gameObject.SetActive(true);
			//}
			for (int i = 0; i < upgradeStatus.Count; i++)
			{
				if (upgradeStatus[i])
				{
					upgradesInfoList[i].SetUpgrade(upgradesInfo[i]);
				}
			}
		}

		// Display the Inventory
		inventoryMenuParent.SetActive(true);
	}

	public void NextPage()
	{
		if (currPage == 0)
		{
			// TODO: Make the Previous Page Button interactable
			prevPageButton.interactable = true;
		}

		currPage++;

		if (currPage == numPages - 1)
		{
			// TODO: Make the Next Page Button uninteractable
			nextPageButton.interactable = false;
		}

		DisplayCurrentPage(inventory.GetItemList());
	}

	public void PrevPage()
	{
		if (currPage == numPages - 1)
		{
			// TODO: Make the Next Page Button interactable
			nextPageButton.interactable = true;
		}

		currPage--;

		if (currPage == 0)
		{
			// TODO: Make the Previous Page Button uninteractable
			prevPageButton.interactable = false;
		}

		DisplayCurrentPage(inventory.GetItemList());
	}

	///////////////////////////////
	// Helpers
	///////////////////////////////
	// Displays all the items in the current page
	private void DisplayCurrentPage(List<Item> _itemList)
	{
		int itemInfoIndex = 0;

		// If the player has an items
		if (_itemList.Count > 0)
		{
			int itemIndex = currPage * numItemSlots;

			// Loop until all item slots are filled OR until the last item in the inventory is reached
			for (; itemInfoIndex < numItemSlots && itemIndex < _itemList.Count; itemInfoIndex++, itemIndex++)
			{
				itemsInfoList[itemIndex].SetItem(_itemList[itemIndex]);
			}
		}

		// TODO: Check to make sure logic works
		// If the are now less items in the list compared to before
		while (itemInfoIndex < numItemSlots)
		{
			// TODO: Think if initializing at beginning and setting them as in/active would be better
			// Hide the info of the previous item
			itemsInfoList[itemInfoIndex].ClearItemInfo();
			itemInfoIndex++;
		}
	}
}
