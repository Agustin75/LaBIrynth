using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	[SerializeField]
	private PuzzleBehavior puzzleManager;
	[SerializeField]
	private BicrossPuzzle[] firstPuzzles;
	[SerializeField]
	private Map firstMapUnlocked;
	[SerializeField]
	private InteractionStep[] firstEvents;

	[Header("Initialize Variables")]
	[SerializeField]
	private ControlTypeVariable currControlType;

	// Start is called before the first frame update
	void Start()
    {
		currControlType.value = ControlType.Labyrinth;
		// TODO: This happens only when the game is first started. Once the game is saved, it will start either in the Labyrinth, or in the last
		//   bicross puzzle the player started (If it hasn't been finished yet)
		// TODO: Move to Tutorial
		//puzzleManager.SetPuzzles(firstPuzzles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// TODO: Handle what happens when a Bicross Puzzle is solved (Move back to Labyrinth if playing there, or to the menu if replaying a puzzle)
}
