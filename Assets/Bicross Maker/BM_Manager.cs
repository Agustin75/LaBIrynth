using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BicrossMaker
{
	public class BM_Manager : BicrossBehavior
	{
		// Scriptable Object to save the puzzle to (Must be assigned before saving)
		[SerializeField]
		private BicrossPuzzle puzzleToModify;

		// Start is called before the first frame update
		private void Start()
		{
			Initialize();
		}

		public override void Initialize()
		{
			base.Initialize();

			// NOTE: These hints automatically update the ScriptableObject, because it links the List inside Hints with the ScriptableObject one
			rowHints = new List<Hints>(rows);
			columnHints = new List<Hints>(columns);

			// Initializes row and columns hints to the default empty line
			for (int i = 0; i < rows; i++)
			{
				rowHintsImages.Add(new List<Image>(rowHintsParent[i].GetComponentsInChildren<Image>()));
				// Removes the parent object
				rowHintsImages[i].RemoveAt(0);
				rowHints.Add(new Hints(new List<HintTypes> { HintTypes.EmptyLine }));
			}
			for (int j = 0; j < columns; j++)
			{
				columnHintsImages.Add(new List<Image>(columnHintsParent[j].GetComponentsInChildren<Image>()));
				// Removes the parent object
				columnHintsImages[j].RemoveAt(0);
				columnHints.Add(new Hints(new List<HintTypes> { HintTypes.EmptyLine }));
			}

			// If there is a puzzle assigned to modify && It's set up
			if (puzzleToModify && puzzleToModify.GetNumRows() != 0)
			{
				// Display the puzzle to the user
				SetPuzzle(puzzleToModify);
			}
		}

		// Update is called once per frame
		void Update()
		{

		}

		public override void SetPuzzle(BicrossPuzzle _puzzle)
		{
			// Grab all the information from the BicrossPuzzle
			rows = _puzzle.GetNumRows();
			columns = _puzzle.GetNumColumns();
			puzzleSolution = _puzzle.GetSolution();
			rowHints = new List<Hints>();
			columnHints = new List<Hints>();
			for (int r = 0; r < rows; r++)
			{
				rowHints.Add(new Hints(new List<HintTypes>(_puzzle.GetRowHints()[r].listOfHints)));
				// Remove the default Empty Line Hints
				for (int h = 0; h < rowHintsImages[r].Count; h++)
				{
					Destroy(rowHintsImages[r][h].gameObject);
				}
				rowHintsImages[r].Clear();
			}
			for (int c = 0; c < rows; c++)
			{
				columnHints.Add(new Hints(new List<HintTypes>(_puzzle.GetColumnHints()[c].listOfHints)));
				// Remove the default Empty Line Hints
				for (int h = 0; h < columnHintsImages[c].Count; h++)
				{
					Destroy(columnHintsImages[c][h].gameObject);
				}
				columnHintsImages[c].Clear();
			}

			// TODO: Add Squares based on the number of rows and columns

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
					// Add the new hint to the array
					columnHintsImages[c].Add(currentHint);
				}
			}

			for (int i = 0; i < puzzle.Count; i++)
			{
				// TODO: Have an Editor-exclusive square that can be changed into the appropriate BicrossSquareState
				puzzle[i].SetSquareState(puzzleSolution[i]);
			}
		}

		public override void SquarePressed(BicrossSquare _square)
		{
			// TODO: Currently empty, might remove
			base.SquarePressed(_square);

			int index = puzzle.FindIndex(i => i == _square);

			if (index == -1)
			{
				Debug.Log("Error: Square wasn't part of the puzzle.");

				return;
			}

			// Get the row and column the Square is in
			int squareRow = index / rows;
			int squareColumn = index % columns;

			// Empty the corresponding row and column hints
			foreach (Image hint in rowHintsImages[squareRow])
			{
				Destroy(hint.gameObject);
			}
			foreach (Image hint in columnHintsImages[squareColumn])
			{
				Destroy(hint.gameObject);
			}

			rowHintsImages[squareRow].Clear();
			columnHintsImages[squareColumn].Clear();
			rowHints[squareRow].listOfHints.Clear();
			columnHints[squareColumn].listOfHints.Clear();

			BicrossSquareState previousState = BicrossSquareState.Undecided;
			BicrossSquareState currentState = BicrossSquareState.Undecided;

			Image currentHint;
			HintTypes currentHintType;

			// Refreshes the columns
			for (int i = 0; i < rows; i++)
			{
				// Save the current state (Saves undecided as empty)
				// Index moves through columns (by multiplying by the amount of rows) with a squareColumn offset
				currentState = puzzle[i * rows + squareColumn].GetSquareContent() == BicrossSquareState.Filled ? BicrossSquareState.Filled : BicrossSquareState.Empty;

				if (currentState != previousState)
				{
					// Get the current HintType
					currentHintType = currentState == BicrossSquareState.Filled ? HintTypes.FilledDot : HintTypes.EmptyDot;
					// Update the column hints
					columnHints[squareColumn].listOfHints.Add(currentHintType);
					// Create a new Hint Object
					currentHint = Instantiate(hintPrefab, columnHintsParent[squareColumn].transform).GetComponent<Image>();
					// Update the sprite to the correct sprite
					currentHint.sprite = spritesHolder.GetHintSprite(currentHintType);
					// Add the new hint to the array
					columnHintsImages[squareColumn].Add(currentHint);
				}
				else
				{
					// Get the current HintType
					currentHintType = currentState == BicrossSquareState.Filled ? HintTypes.FilledLine : HintTypes.EmptyLine;
					// Update the last hint in the column
					columnHints[squareColumn].listOfHints[columnHints[squareColumn].listOfHints.Count - 1] = currentHintType;
					// Update the last sprite to be a line
					columnHintsImages[squareColumn][columnHintsImages[squareColumn].Count - 1].sprite = spritesHolder.GetHintSprite(currentHintType);
				}

				previousState = currentState;
			}

			previousState = BicrossSquareState.Undecided;

			// Refreshes the rows (Moves in reverse because horizontal layout displays them from right to left)
			for (int c = columns - 1; c >= 0; c--)
			{
				// Save the current state (Saves undecided as empty)
				// Index moves through the row with an offset of squareRow * number of columns (The offset moves it to the correct row)
				currentState = puzzle[c + squareRow * columns].GetSquareContent() == BicrossSquareState.Filled ? BicrossSquareState.Filled : BicrossSquareState.Empty;

				if (currentState != previousState)
				{
					// Get the current HintType
					currentHintType = currentState == BicrossSquareState.Filled ? HintTypes.FilledDot : HintTypes.EmptyDot;
					// Update the row hints
					rowHints[squareRow].listOfHints.Add(currentHintType);
					// Create a new Hint Object
					currentHint = Instantiate(hintPrefab, rowHintsParent[squareRow].transform).GetComponent<Image>();
					// Update the sprite to the correct sprite
					currentHint.sprite = spritesHolder.GetHintSprite(currentHintType);
					// Add the new hint to the array
					rowHintsImages[squareRow].Add(currentHint);
				}
				else
				{
					// Get the current HintType
					currentHintType = currentState == BicrossSquareState.Filled ? HintTypes.FilledLine : HintTypes.EmptyLine;
					// Update the last hint in the row
					rowHints[squareRow].listOfHints[rowHints[squareRow].listOfHints.Count - 1] = currentHintType;
					// Update the last sprite to be a line
					rowHintsImages[squareRow][rowHintsImages[squareRow].Count - 1].sprite = spritesHolder.GetHintSprite(currentHintType);
				}

				previousState = currentState;
			}
		}

		public void Save()
		{
			// Reset the previous solution
			puzzleSolution = new List<BicrossSquareState>();

			// Assign bicrossSolution based on whether each Square is Filled or Empty
			for (int i = 0; i < puzzle.Count; i++)
			{
				// Add true if the Square is filled, false otherwise
				puzzleSolution.Add(puzzle[i].GetSquareContent() == BicrossSquareState.Undecided ? BicrossSquareState.Empty : puzzle[i].GetSquareContent());
			}

			// Save the Bicross solution to a ScriptableObject/Prefab
			puzzleToModify.Initialize(rows, columns, puzzleSolution, rowHints, columnHints);
		}
	}
}
