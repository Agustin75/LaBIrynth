using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TileObject : MonoBehaviour
{
	// Returns whether the Object blocks the Player's path
	public virtual bool IsWalkable()
	{
		// By default, an Object won't block the Player's path
		return true;
	}
}
