using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BicrossBehavior : MonoBehaviour
{
	[SerializeField]
	protected int rows, columns;

	// Holds the displayed field
	[SerializeField]
	protected GameObject bicrossField;

	[SerializeField]
	protected GameObject[] rowHintsParent, columnHintsParent;

	[SerializeField]
	protected SpritesHolder spritesHolder;

	[SerializeField]
	protected GameObject hintPrefab;

	protected List<BicrossSquare> puzzle;
	// Holds the final solution to be saved
	protected List<BicrossSquareState> puzzleSolution;

	protected List<List<Image>> rowHintsImages, columnHintsImages;
	// Save the Lists for the row and column's hints
	protected List<Hints> rowHints, columnHints;

	// Start is called before the first frame update
	void Start()
	{

	}

	public virtual void Initialize()
	{
		puzzle = new List<BicrossSquare>(bicrossField.GetComponentsInChildren<BicrossSquare>());
		rowHintsImages = new List<List<Image>>(rows);
		columnHintsImages = new List<List<Image>>(columns);
	}

	// Update is called once per frame
	void Update()
	{

	}

	public virtual void SquarePressed(BicrossSquare _square)
	{

	}

	public virtual void SetPuzzle(BicrossPuzzle _puzzle)
	{
		// Grab all the information from the BicrossPuzzle
		rows = _puzzle.GetNumRows();
		columns = _puzzle.GetNumColumns();
		puzzleSolution = _puzzle.GetSolution();
		// NOTE: Don't modify this ones or it will modify the ScriptableObject
		rowHints = _puzzle.GetRowHints();
		columnHints = _puzzle.GetColumnHints();

		Image currentHint;

		// Loop through the arrays of row hints
		for (int r = 0; r < rows; r++)
		{
			// Loop through each hint on the row
			for (int h = 0; h < rowHints[r].listOfHints.Count; h++)
			{
				// Create a new Hint Object
				currentHint = Instantiate(hintPrefab, rowHintsParent[r].transform).GetComponent<Image>();
				// Update the sprite to the correct sprite
				currentHint.sprite = spritesHolder.GetHintSprite(rowHints[r].listOfHints[h]);
				// Add the new hint to the array
				rowHintsImages[r].Add(currentHint);
			}
		}

		// Loop through the arrays of column hints
		for (int c = 0; c < columns; c++)
		{
			// Loop through each hint on the column
			for (int h = 0; h < columnHints[c].listOfHints.Count; h++)
			{
				// Create a new Hint Object
				currentHint = Instantiate(hintPrefab, columnHintsParent[c].transform).GetComponent<Image>();
				// Update the sprite to the correct sprite
				currentHint.sprite = spritesHolder.GetHintSprite(columnHints[c].listOfHints[h]);
				currentHint.transform.Rotate(0, 0, 90);
				// Add the new hint to the array
				columnHintsImages[c].Add(currentHint);
			}
		}
	}
}
