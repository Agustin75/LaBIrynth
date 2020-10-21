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
	private GameObject warpObjectsParent;

	// Prefab for each Teleporter Item to display on the Menu
	[SerializeField]
	private GameObject warpMenuItemPrefab;

	[SerializeField]
	private WarpSO currWarp;

	[SerializeField]
	private BoolVariable isPlayerTeleporting;

	// Hold the Warp Menu Items to make them un/interactable later
	private List<WarpMenuItem> warpMenuItems;

	// Holds a reference to all the existing warps in the Labyrinth
	private List<Warp> warpsList;

	private int numOfWarps;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Initialize()
    {
		warpMenuItems = new List<WarpMenuItem>();

		warpsList = new List<Warp>(warpObjectsParent.GetComponentsInChildren<Warp>());
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
