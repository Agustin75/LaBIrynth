using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	// Parent to spawn the popup panels from the Interaction Steps
	[SerializeField]
	private Transform popupParent;

	[SerializeField]
	private GameObject obstacleUnpassablePopup;

	[SerializeField]
	private GatePopup gateUnpassablePopup;

	[SerializeField]
	private PuzzleBehavior puzzleManager;

	[SerializeField]
	private MapManager mapsManager;

	[Header("Scriptable Objects")]
	[SerializeField]
	private ControlTypeVariable currentControlType;

	// Interactable Object the player is currently in contact with
	private InteractableObject objectInContact;

	// Whether the object can be interacted with
	private bool canInteract;

	private GameObject stepPopup;

	private InteractionStep[] stepsToComplete;
	private int currentStep;

	public void SetObjectInContact(InteractableObject _object)
	{
		objectInContact = _object;
		stepsToComplete = objectInContact.GetSteps();
		canInteract = objectInContact.CanInteract();
	}

	// The player is no longer in contact with the InteractableObject
	public void ObjectLostContact()
	{
		objectInContact = null;
		canInteract = false;
	}

	public void Interact()
	{
		// No object to interact with
		if (!objectInContact)
		{
			return;
		}

		// Something is required to get past this Object
		if (!canInteract)
		{
			switch (objectInContact.GetObjectType())
			{
				case InteractableObjectTypes.Gate:
					Gate gateObject = (Gate)objectInContact;
					// Display the rune(s) required
					gateUnpassablePopup.ShowPopup(gateObject.GetItemRequired());
					// Display the "Can't progress" message
					gateUnpassablePopup.gameObject.SetActive(true);
					break;
				case InteractableObjectTypes.Obstacle:
					// TODO: Might have to add a way to show the player what he needs before they can pass through here (be it powerups or runes,
					//		and whether it should specify which one in particular is needed)
					obstacleUnpassablePopup.SetActive(true);
					break;
			}

			// Set control to menu
			currentControlType.value = ControlType.Menu;

			return;
		}

		// Call the first step
		ExecuteStep();
	}

	public void NextStep()
	{
		// TODO: Rethink: Called if the NextStep Event is sent and the InteractionManager doesn't have any steps to follow
		// TODO: Incidentally, could listen for certain Events (such as "PuzzleSolved"), and move to the next step if it applies
		if (stepsToComplete == null)
		{
			return;
		}

		ExitStep();

		// Move on to the next Step
		currentStep++;

		if (currentStep >= stepsToComplete.Length)
		{
			// TODO: All steps completed, go back to Labyrinth?

			stepsToComplete = null;

			currentStep = 0;

			return;
		}

		// TODO: Might have to add a pause here

		ExecuteStep();
	}

	public void PuzzlesCompleted()
	{
		// If there is currently no step set up || The current step is not a Puzzle step
		if (stepsToComplete == null || stepsToComplete[currentStep].stepType != InteractionStepTypes.Puzzle)
		{
			return;
		}

		NextStep();
	}

	public void MapUnlocked()
	{
		// If there is currently no step set up || The current step is not an Unlock Map step
		if (stepsToComplete == null || stepsToComplete[currentStep].stepType != InteractionStepTypes.UnlockMap)
		{
			return;
		}

		NextStep();
	}

	public void ObstacleRemoved()
	{
		// If there is currently no step set up || The current step is not an Passed Obstacle step
		if (stepsToComplete == null || stepsToComplete[currentStep].stepType != InteractionStepTypes.PassedObstacle)
		{
			return;
		}

		NextStep();
	}

	public void ClosePopup()
	{
		// If there is currently no step set up || The current step is not a Popup step
		if (stepsToComplete == null || stepsToComplete[currentStep].stepType != InteractionStepTypes.PopUp)
		{
			return;
		}

		NextStep();
	}

	public void ExitInteraction()
	{
		ExitStep();
		ObjectLostContact();
		currentControlType.value = ControlType.Labyrinth;
	}

	public void CloseGateUnpassablePopup()
	{
		// Hide the Gate Unpassable Popup
		gateUnpassablePopup.gameObject.SetActive(false);

		// Set the control type back to Labyrinth
		currentControlType.value = ControlType.Labyrinth;
	}

	public void CloseObstacleUnpassablePopup()
	{
		// Hide the Obstacle Unpassable Popup
		obstacleUnpassablePopup.SetActive(false);

		// Set the control type back to Labyrinth
		currentControlType.value = ControlType.Labyrinth;
	}

	//////////////////////////
	// Helpers
	//////////////////////////
	// Clears all variables set on this step
	private void ExitStep()
	{
		switch (stepsToComplete[currentStep].stepType)
		{
			case InteractionStepTypes.PopUp:
				// Destroy the popup instantiated
				Destroy(stepPopup);
				// Reset the saved popup
				stepPopup = null;
				currentControlType.value = ControlType.Labyrinth;
				break;
			case InteractionStepTypes.Puzzle:
				// TODO: Might have to rewrite if it's used for menu puzzles as well (which shouldn't be the case)
				// Revert the Control Type back to Labyrinth
				currentControlType.value = ControlType.Labyrinth;
				break;
			case InteractionStepTypes.PassedObstacle:
				// TODO: Clear the Obstacle steps variables, if any
				break;
			case InteractionStepTypes.Story:
				// TODO: Clear the Story steps variables, if any
				break;
			case InteractionStepTypes.UnlockMap:
				// TODO: Clear the Unlock Map steps variables, if any
				break;
			default:
				break;
		}
	}

	// Handles what happens in this step
	private void ExecuteStep()
	{
		switch (stepsToComplete[currentStep].stepType)
		{
			case InteractionStepTypes.PopUp:
				// TODO: Display appropriate Popup
				stepPopup = Instantiate(stepsToComplete[currentStep].popupPanel, popupParent);
				currentControlType.value = ControlType.Menu;
				break;
			case InteractionStepTypes.Puzzle:
				// Tell PuzzleBehavior to show the Puzzles
				puzzleManager.SetPuzzles(stepsToComplete[currentStep].puzzleList);
				// Change the control type to Bicross
				currentControlType.value = ControlType.Bicross;
				break;
			case InteractionStepTypes.PassedObstacle:
				// TODO: Tell obstacle to perform its Animation?
				// TODO: Temporary, change to correct implementation once implemented (Enemy fleeing, obstacle disappearing, etc)
				//Destroy(objectInContact.gameObject);
				// ERROR: Check this, sometimes it's null
				objectInContact.Passed();

				objectInContact = null;

				// TODO: Temporary code, put after animation or however else it's implemented
				NextStep();
				break;
			case InteractionStepTypes.Story:
				// TODO: Tell StoryManager to play the appropriate Story
				break;
			case InteractionStepTypes.UnlockMap:
				// TODO: Play the Unlock Map animation
				mapsManager.UnlockMap(stepsToComplete[currentStep].mapToUnlock);
				break;
			default:
				break;
		}
	}
}
