using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : InteractableObject
{
	[Header("Scriptable Objects")]
	[SerializeField]
	private BoolVariable runeUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	// Successfully passed by the object (Defeated the enemy, obtained the rune, etc)
	public override void Passed()
	{
		// Set the corresponding Rune to solved
		runeUnlocked.value = true;

		base.Passed();
	}

	public override InteractableObjectTypes GetObjectType()
	{
		return InteractableObjectTypes.Rune;
	}
}
