using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TileBehavior))]
public class TileEditor : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		TileBehavior tile = (TileBehavior)target;
		tile.UpdateTileSprite();
	}
}
