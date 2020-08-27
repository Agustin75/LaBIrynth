using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
	[SerializeField]
	private PlayerMovement movement;

	[Header("Scriptable Objects")]
	[SerializeField]
	private ControlTypeVariable currControlType;

	[Header("Events")]
	[SerializeField]
	private GameEvent interactEvent;
	[SerializeField]
	private GameEvent openInventoryEvent;

	private int xMovement, yMovement;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		switch (currControlType.value)
		{
			case ControlType.Bicross:
				HandleBicrossInput();
				break;
			case ControlType.Labyrinth:
				HandleLabyrinthInput();
				break;
			case ControlType.Menu:
				HandleMenuInput();
				break;
			default:
				break;
		}
	}

	private void HandleBicrossInput()
	{

	}

	private void HandleLabyrinthInput()
	{
		if (Input.GetKey(KeyCode.UpArrow))
		{
			yMovement = 1;
		}
		else if (Input.GetKey(KeyCode.DownArrow))
		{
			yMovement = -1;
		}
		else
		{
			yMovement = 0;
		}

		if (Input.GetKey(KeyCode.LeftArrow))
		{
			xMovement = -1;
		}
		else if (Input.GetKey(KeyCode.RightArrow))
		{
			xMovement = 1;
		}
		else
		{
			xMovement = 0;
		}

		movement.Move(xMovement, yMovement);

		// TODO: Change to correct input type (whether mobile, pc, etc)
		if (Input.GetKeyDown(KeyCode.Return))
		{
			InteractPressed();
		}

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			// Open inventory
			openInventoryEvent.Raise();

			currControlType.value = ControlType.Menu;
		}
	}

	private void HandleMenuInput()
	{

	}

	public void InteractPressed()
	{
		// Raise the interact event (TODO: Might add an interaction sound eventually)
		interactEvent.Raise();
	}

	//////////////////////////////////
	/// Game Event functions
	//////////////////////////////////
	public void SetMenuControl()
	{
		currControlType.value = ControlType.Menu;
	}

	public void SetLabyrinthControl()
	{
		currControlType.value = ControlType.Labyrinth;
	}
}
