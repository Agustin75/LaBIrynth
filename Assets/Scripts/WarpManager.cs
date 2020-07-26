using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpManager : MonoBehaviour
{
	[SerializeField]
	private GameObject warpMenu;

	[SerializeField]
	private GameObject playerObject;

	// Parent to all the warp menu items
	[SerializeField]
	private Transform warpMenuItemsParent;

	[SerializeField]
	private List<Warp> warpsList;

	// Prefab for each Teleporter Item to display on the Menu
	[SerializeField]
	private GameObject warpMenuItemPrefab;

	[SerializeField]
	private WarpSO currWarp;

	[SerializeField]
	private BoolVariable isPlayerTeleporting;

	// Hold the Warp Menu Items to make them un/interactable later
	private List<WarpMenuItem> warpMenuItems;

	private int numOfWarps;

    // Start is called before the first frame update
    void Start()
    {
		warpMenuItems = new List<WarpMenuItem>();

		numOfWarps = warpsList.Count;

		foreach (Warp warp in warpsList)
		{
			// Instantiate the Menu Item
			WarpMenuItem item = Instantiate(warpMenuItemPrefab, warpMenuItemsParent).GetComponent<WarpMenuItem>();

			// Initialize the menu item
			item.Initialize(warp, warp.IsWarpActive());

			// Add the new Warp Menu Item to the list
			warpMenuItems.Add(item);
		}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	// TODO: Add a function to set all Warps to Active based on the saved data

	// TODO: Add a function to send all Warps to save their active status on a file

	public void OpenWarpMenu()
	{
		for (int i = 0; i < numOfWarps; i++)
		{
			// Set all Warps other than the warp entered through as interactable
			warpMenuItems[i].SetInteractable(warpsList[i].GetWarpID() != currWarp.GetWarpID());
		}

		// Display the Teleporter Menu
		warpMenu.SetActive(true);
	}

	public void OnTeleport()
	{
		isPlayerTeleporting.value = true;

		// Teleport the player to the selected warp
		playerObject.transform.position = warpsList[currWarp.warpID].transform.position;

		// Close the warp menu
		warpMenu.SetActive(false);
	}

	public void OnWarpActivated()
	{
		// Display the newly activated Warp as an option
		warpMenuItems[currWarp.GetWarpID()].Activate();
	}
}
