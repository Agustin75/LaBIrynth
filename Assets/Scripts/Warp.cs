using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : SaveObject
{
	[SerializeField]
	private Camera warpCamera;

	[SerializeField]
	private int floorNumber, warpID;

	[SerializeField]
	private SpriteRenderer warpImage;

	[SerializeField]
	private SpritesHolder spritesHolder;

	private bool warpActive = false;

    // Start is called before the first frame update
    void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Activate
	// -------------------------
	// Activates the warp for the first time (Plays sound, animation and etc)
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void Activate()
	{
		warpActive = true;

		// Change the warp image to the active version
		warpImage.sprite = spritesHolder.GetWarpSprite(warpActive);
	}

	/////////////////////////////////////////////////////////////////////////////
	// Inheritable Functions
	/////////////////////////////////////////////////////////////////////////////
	public override void SetObjectState(bool _status)
	{
		warpActive = _status;
		warpImage.sprite = spritesHolder.GetWarpSprite(warpActive);
	}

	public override bool GetCurrentState()
	{
		return warpActive;
	}

	//////////////////////////
	// Accessors
	//////////////////////////
	public int GetFloorNumber()
	{
		return floorNumber;
	}

	public int GetWarpID()
	{
		return warpID;
	}

	public bool IsWarpActive()
	{
		return warpActive;
	}

	public RenderTexture GetWarpTexture()
	{
		return warpCamera.targetTexture;
	}
}
