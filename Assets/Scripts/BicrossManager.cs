using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BicrossManager : BicrossBehavior
{
	[SerializeField]
	private GameEvent puzzleSolved;

	public override void Initialize()
	{
		base.Initialize();

		for (int i = 0; i < rows; i++)
		{
			rowHintsImages.Add(new List<Image>());
		}
		for (int j = 0; j < columns; j++)
		{
			columnHintsImages.Add(new List<Image>());
		}
	}

	public override void SquarePressed(BicrossSquare _square)
	{
		int index = puzzle.FindIndex(i => i == _square);

		if (index == -1)
		{
			Debug.Log("Error: Square wasn't part of the puzzle.");

			return;
		}

		// Loop through the game and solution boards
		for (int i = 0; i < rows * columns; i++)
		{
			// If the contents are different && one of them is incorrectly filled (Empty and Undecided both count as Empty)
			if (puzzle[i].GetSquareContent() != puzzleSolution[i] &&
				(puzzle[i].GetSquareContent() == BicrossSquareState.Filled || puzzleSolution[i] == BicrossSquareState.Filled))
			{
				return;
			}
		}

		// Raise the Puzzle Solved Event
		puzzleSolved.Raise();

		for (int i = 0; i < rows * columns; i++)
		{
			puzzle[i].DisableButton();
		}
	}
}
