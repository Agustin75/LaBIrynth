using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : InteractableObject
{
	[Header("Scriptable Objects")]
	[SerializeField]
	// Rune(s) that unlocks this Gate
	private BoolVariable[] unlockRune;

	private int ID;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public override bool CanInteract()
	{
		foreach (bool rune in unlockRune)
		{
			if (!rune)
			{
				return false;
			}
		}

		return base.CanInteract();
	}

	public override InteractableObjectTypes GetObjectType()
	{
		return InteractableObjectTypes.Gate;
	}
}
