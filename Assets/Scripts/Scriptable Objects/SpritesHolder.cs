using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileLayouts", menuName = "Tile Layouts")]
public class SpritesHolder : ScriptableObject
{
	[Header("Selected Themes")]
	[SerializeField]
	private Themes labyrinthTheme;
	[SerializeField]
	private Themes bicrossTheme;

	[Header("Labyrinth Sprites")]
	[SerializeField]
	private Sprite[] wallTiles;
	[SerializeField]
	private Sprite[] groundTiles;
	[SerializeField]
	private Sprite[] inactiveWarps;
	[SerializeField]
	private Sprite[] activeWarps;

	[Header("Bicross Sprites")]
	[SerializeField]
	private Sprite[] filledSquareSprite;
	[SerializeField]
	private Sprite[] emptySquareSprite;
	[SerializeField]
	private Sprite[] emptyDotSprite;
	[SerializeField]
	private Sprite[] emptyLineSprite;
	[SerializeField]
	private Sprite[] filledDotSprite;
	[SerializeField]
	private Sprite[] filledLineSprite;

	public Sprite GetWallTile()
	{
		return wallTiles[(int)labyrinthTheme];
	}

	public Sprite GetGroundTile()
	{
		return groundTiles[(int)labyrinthTheme];
	}

	public Sprite GetWarpSprite(bool _active)
	{
		return _active ? activeWarps[(int)labyrinthTheme] : inactiveWarps[(int)labyrinthTheme];
	}

	public Sprite GetSquareSprite(BicrossSquareState _state)
	{
		switch (_state)
		{
			case BicrossSquareState.Empty:
				return emptySquareSprite[(int)bicrossTheme];
			case BicrossSquareState.Filled:
				return filledSquareSprite[(int)bicrossTheme];
		}
		return null;
	}

	public Sprite GetHintSprite(HintTypes _hint)
	{
		switch (_hint)
		{
			case HintTypes.EmptyDot:
				return emptyDotSprite[(int)bicrossTheme];
			case HintTypes.EmptyLine:
				return emptyLineSprite[(int)bicrossTheme];
			case HintTypes.FilledDot:
				return filledDotSprite[(int)bicrossTheme];
			case HintTypes.FilledLine:
				return filledLineSprite[(int)bicrossTheme];
		}
		return null;
	}
}
