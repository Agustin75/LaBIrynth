using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rune : InteractableObject
{
	[Header("Scriptable Objects")]
	[SerializeField]
	private Item runeInfo;

	[SerializeField]
	private InventoryManager inventoryManager;

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
		inventoryManager.AddItem(runeInfo);

		base.Passed();
	}

	public override InteractableObjectTypes GetObjectType()
	{
		return InteractableObjectTypes.Rune;
	}
}
