using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Bool Variable", menuName = "Variable/Bool")]
public class BoolVariable : ScriptableObject
{
	public bool value;

	public static implicit operator bool(BoolVariable reference)
	{
		return reference.value;
	}
}
