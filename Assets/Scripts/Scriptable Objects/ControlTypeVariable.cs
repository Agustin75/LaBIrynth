using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Control Type Variable", menuName = "Variable/Control Type")]
public class ControlTypeVariable : ScriptableObject
{
	public ControlType value;

	public static implicit operator ControlType(ControlTypeVariable reference)
	{
		return reference.value;
	}
}
