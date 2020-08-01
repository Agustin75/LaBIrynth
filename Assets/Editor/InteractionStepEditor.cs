using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(InteractionStep))]
public class InteractionStepEditor : Editor
{
	public override void OnInspectorGUI()
	{
		// Get the Object
		InteractionStep step = (InteractionStep)target;

		// "step.stepType = " makes it so the value selected in the inspector is saved to the variable
		// "EditorGUILayout.EnumPopup" makes the Enum field and to show in the Inspector
		step.stepType = (InteractionStepTypes)EditorGUILayout.EnumPopup("Step Type", step.stepType);

		switch (step.stepType)
		{
			case InteractionStepTypes.PopUp:
				// Display the Popups field
				step.popupPanel = (GameObject)EditorGUILayout.ObjectField("Popup Panel", step.popupPanel, typeof(GameObject), true);
				break;
			case InteractionStepTypes.Puzzle:
				// Get the array from the Object
				SerializedProperty puzzlesArray = serializedObject.FindProperty("puzzleList");
				// Displays the array on the Inspector
				EditorGUILayout.PropertyField(puzzlesArray, true);
				break;
			case InteractionStepTypes.PassedObstacle:
				// TODO: Display the Obstacle field (If any)
				break;
			case InteractionStepTypes.Story:
				// TODO: Display the Story field
				break;
			case InteractionStepTypes.UnlockMap:
				step.mapToUnlock = (int)EditorGUILayout.IntField("Map To Unlock", step.mapToUnlock);
				break;
		}

		// This applies the changes made to array Elements, among other things
		serializedObject.ApplyModifiedProperties();

		// So it saves the changes made
		EditorUtility.SetDirty(target);
	}
}
