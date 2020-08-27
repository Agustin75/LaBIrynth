using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : InteractableObject
{
	[Header("Scriptable Objects")]

	[SerializeField]
	// Rune(s) that unlocks this Gate
	private Item runeRequired;

	[SerializeField]
	private InventoryManager inventoryManager;

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
		if (!inventoryManager.ContainsItem(runeRequired))
		{
			return false;
		}

		return base.CanInteract();
	}

	public override void Passed()
	{
		// Remove them from the player's Inventory
		inventoryManager.RemoveItem(runeRequired);

		base.Passed();
	}
	
	public override InteractableObjectTypes GetObjectType()
	{
		return InteractableObjectTypes.Gate;
	}

	public Item GetItemRequired()
	{
		return runeRequired;
	}
}
