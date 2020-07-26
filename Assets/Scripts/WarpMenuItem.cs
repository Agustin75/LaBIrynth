using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WarpMenuItem : MonoBehaviour
{
	[SerializeField]
	private GameObject lockedObject;
	[SerializeField]
	private RawImage warpImage;
	[SerializeField]
	private Text warpLocation;
	[SerializeField]
	private Button warpItemButton;

	[Header("Scriptable Objects")]
	[SerializeField]
	private WarpSO currentWarp;

	[Header("Events")]
	[SerializeField]
	private GameEvent onTeleport;

	// Holds the warp this item refers to
	private Warp warpDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize(Warp _warp, bool _active)
	{
		warpDisplayed = _warp;

		// Sets whether the Warp is available
		lockedObject.SetActive(!_active);

		// Add the camera render texture to the image displayed
		warpImage.texture = warpDisplayed.GetWarpTexture();

		// Assign the warpLocation
		warpLocation.text = "Warp " + (warpDisplayed.GetWarpID() + 1) + " (Map " + (warpDisplayed.GetFloorNumber() + 1) + ")";
	}

	// Called when a new Warp is activated
	public void Activate()
	{
		// Hide the Locked Warp Sprite
		lockedObject.SetActive(false);
		// Make the button interactable
		warpItemButton.interactable = true;
	}

	public void OnWarpSelected()
	{
		// Update the current warp
		currentWarp.warpID = warpDisplayed.GetWarpID();

		// Raise the OnTeleport Event
		onTeleport.Raise();
	}

	public void SetInteractable(bool _interactable)
	{
		// Check whether the Warp has been activated before changing its interactable field
		warpItemButton.interactable = warpDisplayed.IsWarpActive() && _interactable;
	}
}
