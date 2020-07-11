using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
	// Parent to spawn the popup panels from the Interaction Steps
	[SerializeField]
	private Transform popupParent;

	[SerializeField]
	private PuzzleBehavior puzzleManager;

	[Header("Scriptable Objects")]
	[SerializeField]
	private ControlTypeVariable currentControlType;

	// Interactable Object the player is currently in contact with
	private InteractableObject objectInContact;

	// TODO: Either use a BoolVariable or have some other way to grab any type of requirement
	// Object required to get past the Interactable Object (Upgrade, Key, etc)
	private GameObject requirement;

	private GameObject stepPopup;

	private InteractionStep[] stepsToComplete;
	private int currentStep;

	public void SetObjectInContact(InteractableObject _object)
	{
		objectInContact = _object;
		stepsToComplete = objectInContact.GetSteps();

		// TODO: Add the item to "requirement", if it has any
	}

	// The player is no longer in contact with the InteractableObject
	public void ObjectLostContact()
	{
		objectInContact = null;
		requirement = null;
	}

	public void Interact()
	{
		// No object to interact with
		if (!objectInContact)
		{
			return;
		}

		// Something is required to get past this Object
		if (requirement)
		{
			// TODO: Check if the player has the required item

			// TODO: If the player doesn't have it, display the "Can't progress" message
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

		switch (stepsToComplete[currentStep].stepType)
		{
			case InteractionStepTypes.PopUp:
				// Destroy the popup instantiated
				Destroy(stepPopup);
				// Reset the saved popup
				stepPopup = null;
				break;
			case InteractionStepTypes.Puzzle:
				// TODO: Might have to rewrite if it's used for menu puzzles as well (which shouldn't be the case)
				// Revert the Control Type back to Labyrinth
				currentControlType.value = ControlType.Labyrinth;
				break;
			case InteractionStepTypes.RemoveObstacle:
				// TODO: Clear the Obstacle steps variables, if any
				break;
			case InteractionStepTypes.Story:
				// TODO: Clear the Story steps variables, if any
				break;
			default:
				break;
		}

		// Move on to the next Step
		currentStep++;

		if (currentStep >= stepsToComplete.Length)
		{
			// TODO: All steps completed, go back to Labyrinth?

			stepsToComplete = null;

			return;
		}

		// TODO: Might have to add a pause here

		ExecuteStep();
	}

	public void PuzzleSolved()
	{
		// If there is currently no step set up || The current step is not a Puzzle step
		if (stepsToComplete == null || stepsToComplete[currentStep].stepType != InteractionStepTypes.Puzzle)
		{
			return;
		}

		NextStep();
	}

	//////////////////////////
	// Helpers
	//////////////////////////
	// Handles what happens in this step
	private void ExecuteStep()
	{
		switch (stepsToComplete[currentStep].stepType)
		{
			case InteractionStepTypes.PopUp:
				// TODO: Display appropriate Popup
				stepPopup = Instantiate(stepsToComplete[currentStep].popupPanel, popupParent);
				break;
			case InteractionStepTypes.Puzzle:
				// Tell PuzzleBehavior to show the Puzzles
				puzzleManager.SetPuzzles(stepsToComplete[currentStep].puzzleList);
				// Change the control type to Bicross
				currentControlType.value = ControlType.Bicross;
				break;
			case InteractionStepTypes.RemoveObstacle:
				// TODO: Tell obstacle to perform its Animation?
				// TODO: Temporary, change to correct implementation once implemented (Enemy fleeing, obstacle disappearing, etc)
				Destroy(objectInContact.gameObject);
				objectInContact = null;
				break;
			case InteractionStepTypes.Story:
				// TODO: Tell StoryManager to play the appropriate Story
				break;
			default:
				break;
		}
	}
}
