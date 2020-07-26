using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
	[SerializeField]
	private Camera warpCamera;

	[SerializeField]
	private int floorNumber, warpID;

	[SerializeField]
	private SpriteRenderer warpImage;

	[SerializeField]
	private SpritesHolder spritesHolder;

	private bool warpActive;

    // Start is called before the first frame update
    void Start()
    {
		warpActive = false;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Activate()
	{
		warpActive = true;

		// Change the warp image to the active version
		warpImage.sprite = spritesHolder.GetWarpSprite(warpActive);
	}

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
