using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class InteractionStep : ScriptableObject
{
	public InteractionStepTypes stepType;
	public BicrossPuzzle[] puzzleList;
	public GameObject popupPanel;
	// TODO: Add a variable for story ScriptableObjects
}
