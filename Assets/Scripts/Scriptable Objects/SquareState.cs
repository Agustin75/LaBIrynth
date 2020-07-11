using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SquareState", menuName = "Square State")]
public class SquareState : ScriptableObject
{
	public BicrossSquareState value;

	public static implicit operator BicrossSquareState(SquareState reference)
	{
		return reference.value;
	}
}
