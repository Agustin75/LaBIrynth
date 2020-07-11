using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Hints
{
	[SerializeField]
	public List<HintTypes> listOfHints;

	public Hints(List<HintTypes> _values = null)
	{
		if (_values != null)
			listOfHints = new List<HintTypes>(_values);
		else
			listOfHints = new List<HintTypes>();
	}

	public static implicit operator List<HintTypes>(Hints reference)
	{
		return reference.listOfHints;
	}
}
