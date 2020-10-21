using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject, IComparable<Item>
{
	[SerializeField]
	private ItemType itemType;
	[SerializeField]
	private Sprite itemSprite;

	[SerializeField]
	[HideInInspector]
	// Needs to be saved so the so it always stays the same between Editor sessions
	private int ID = -1;

	private int sortID;
	private string name, description;

	public int CompareTo(Item other)
	{
		if (itemType != other.itemType)
			return itemType.CompareTo(other.itemType);

		return sortID.CompareTo(other.sortID);
	}

	public Sprite GetItemSprite()
	{
		return itemSprite;
	}

	// NOTE: DEBUG FUNCTIONS ONLY!
	public int GetItemID()
	{
		return ID;
	}

	public void SetItemID(int _id)
	{
		ID = _id;
	}
}
