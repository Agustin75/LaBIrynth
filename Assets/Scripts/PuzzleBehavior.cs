using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBehavior : MonoBehaviour
{
	// Holds the transform to the parent of the Bicross puzzles
	[SerializeField]
	private Transform bicrossParentTransform;

	// bicrossCanvasParent holds every item to be shown to the player while on bicross mode
	[SerializeField]
	private GameObject bicrossCanvasParent, continuePanel;

	// Holds every puzzle variation
	[SerializeField]
	private BicrossManager[] puzzlePrefabs;

	[Header("Scriptable Objects")]
	[SerializeField]
	private ControlTypeVariable currentControlType;

	[SerializeField]
	private SquareState currentFillType;

	private BicrossPuzzle[] puzzlesToComplete;
	private int currentPuzzle;

	// Start is called before the first frame update
	void Start()
	{
		currentPuzzle = 0;
	}

    // Update is called once per frame
    void Update()
    {

	}

	// Save the puzzles the player needs to complete to complete to defeat the enemy/unlock the next Labyrinth room
	public void SetPuzzles(BicrossPuzzle[] _puzzles)
	{
		puzzlesToComplete = _puzzles;
		currentPuzzle = 0;
		ShowPuzzle(puzzlesToComplete[currentPuzzle]);
	}

	public virtual void ToggleFiller()
	{
		currentFillType.value = currentFillType == BicrossSquareState.Filled ? BicrossSquareState.Empty : BicrossSquareState.Filled;
	}

	// Function invoked through event
	public void PuzzleSolved()
	{
		// Increment the amount of puzzles cleared
		currentPuzzle++;

		// The puzzle was solved, show the panel to move to the next step
		continuePanel.SetActive(true);
	}

	// Called when the player clicks Continue after clearing a puzzle
	public void NextStep()
	{
		// Hide the Cleared Puzzle panel
		continuePanel.SetActive(false);

		// If all the Puzzles were cleared
		if (currentPuzzle == puzzlesToComplete.Length)
		{
			currentPuzzle = 0;

			// Hide the Bicross UI
			bicrossCanvasParent.SetActive(false);

			// TODO: Change this to account for whether the puzzle was a Labyrinth one or a menu one
			// Change to the correct input type
			currentControlType.value = ControlType.Labyrinth;

			return;
		}

		// Show the next puzzle to the player
		// TODO: Will need to pause to show the player the feedback (Maybe shrink and move the puzzle to the top right of the screen, 
		//   to show the completed version later
		ShowPuzzle(puzzlesToComplete[currentPuzzle]);
	}

	// Show the next puzzle to the player
	private void ShowPuzzle(BicrossPuzzle _puzzle)
	{
		// Get the Puzzle's Size and instantiate the correct object
		BicrossSize size = _puzzle.GetBicrossSize();
		// Instantiate the puzzle object
		BicrossManager instantiatedPuzzle = Instantiate(puzzlePrefabs[(int)size], bicrossParentTransform);
		// Prepare the puzzle (Initialize Lists, etc)
		instantiatedPuzzle.Initialize();
		// Set the puzzle to show the player
		instantiatedPuzzle.SetPuzzle(_puzzle);
		// Display the puzzle to the player
		instantiatedPuzzle.gameObject.SetActive(true);
		// TODO: Set the toggle button to the be same as the Toggle variable?
		// Show the bicross Canvas
		bicrossCanvasParent.SetActive(true);
	}
}
