using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Warp")]
public class WarpSO : ScriptableObject
{
	public int warpID;
	
	public int GetWarpID()
	{
		return warpID;
	}
}
