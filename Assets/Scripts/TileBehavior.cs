using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileBehavior : MonoBehaviour
{
	[SerializeField]
	private Image tileImage;

	// Add type of Tile (Ground, Wall, etc)
	[SerializeField]
	private TileType tileType;

	[SerializeField]
	private TileBehavior[] tileConnections;

	//// TODO: Add item present on this Tile, if any
	//[SerializeField]
	//private Item tileItem;

	//// TODO: Add enemy in this Tile, if any
	//[SerializeField]
	//private Enemy tileEnemy;

	//// Add Obstacle in this Tile, if any
	//[SerializeField]
	//private Obstacle tileObstacle;

	// Add the Object present in this Tile, if any
	[SerializeField]
	private TileObject tileObject;

	[SerializeField]
	private SpritesHolder tileLayout;

    // Start is called before the first frame update
    void Start()
    {
		UpdateTileSprite();
	}

	// Returns whether the player can walk on this tile
	public bool IsWalkable()
	{
		// If Tile is Wall
		if (tileType == TileType.Wall)
			return false;

		// TODO: If Tile is Pit and player doesn't have Levitating, return false
		if (tileType == TileType.Pit)
			return false;

		// TODO: If Tile has an Enemy, return false

		// TODO: If Tile has an obstacle and player doesn't have corresponding Upgrade, return false

		// TODO: If Tile has a closed Gate, return false

		return true;
	}

	public void Interact()
	{
		// If Tile is a wall
		if (tileType == TileType.Wall)
			// (Nothing to interact with)
			return;

		// TODO: If Tile has an obstable, display correct behavior

		// TODO: If Tile has Enemy, display correct behavior
	}

	public void UpdateTileSprite()
	{
		switch (tileType)
		{
			case TileType.Ground:
				tileImage.sprite = tileLayout.GetGroundTile();
				break;
			case TileType.Pit:
				break;
			case TileType.Wall:
				tileImage.sprite = tileLayout.GetWallTile();
				break;
		}
	}
}
