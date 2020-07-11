using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BicrossPuzzle))]
public class BicrossPuzzleEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		// Needed because Unity doesn't automatically Dirty it when changing the value through code
		EditorUtility.SetDirty(target);
	}
}
