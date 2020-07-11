using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TileLayouts", menuName = "Tile Layouts")]
public class SpritesHolder : ScriptableObject
{
	[SerializeField]
	private Themes tileThemes, bicrossTheme;

	[SerializeField]
	private Sprite[] wallTiles;
	[SerializeField]
	private Sprite[] groundTiles;

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
		return wallTiles[(int)tileThemes];
	}

	public Sprite GetGroundTile()
	{
		return groundTiles[(int)tileThemes];
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
