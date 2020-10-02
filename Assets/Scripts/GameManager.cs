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
	[SerializeField]
	private FloorManager[] floorManagers;

	[Header("Scriptable Objects")]
	[SerializeField]
	private SaveManager saveManager;
	[SerializeField]
	private UpgradesManager upgradesManager;

	[Header("Initialize Variables")]
	[SerializeField]
	private ControlTypeVariable currControlType;

	// Start is called before the first frame update
	void Start()
    {
		currControlType.value = ControlType.Labyrinth;

		// Try to load the last saved file
		saveManager.LoadFile();

		// Initialize all Managers
		foreach (FloorManager floor in floorManagers)
		{
			floor.Initialize();
			upgradesManager.Initialize();
		}

		//if (saveManager.IsNewGame())
		//{
		//	// TODO: This happens only when the game is first started. Once the game is saved, it will start either in the Labyrinth, or in the last
		//	//   bicross puzzle the player started (If it hasn't been finished yet)

		//	// TODO: Move to Tutorial
		//	//puzzleManager.SetPuzzles(firstPuzzles);
		//}
	}

    // Update is called once per frame
    void Update()
    {
        
    }

	// If the app is closed without going to the Main Menu
	private void OnApplicationQuit()
	{
		SaveGame();
	}

	public void SaveGame()
	{
		// Tell FloorManager to save the objects states
		foreach (FloorManager floor in floorManagers)
		{
			floor.SaveInformation();
		}

		upgradesManager.SaveInformation();

		// Save the game to file
		saveManager.SaveFile();
	}

	// TODO: Handle what happens when a Bicross Puzzle is solved (Move back to Labyrinth if playing there, or to the menu if replaying a puzzle)
}
