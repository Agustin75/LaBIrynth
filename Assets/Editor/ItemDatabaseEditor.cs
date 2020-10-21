using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ItemDatabase))]
public class ItemDatabaseEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		if (GUILayout.Button("Set Items IDs"))
		{
			ItemDatabase database = (ItemDatabase)target;
			database.SetItemsIDs();
		}

		if (GUILayout.Button("Reset Item IDs"))
		{
			ItemDatabase database = (ItemDatabase)target;
			database.SetItemsIDs(true);
		}

		// Needed because Unity doesn't automatically Dirty it when changing the value through code
		EditorUtility.SetDirty(target);
	}
}
