using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject, IComparable<Item>
{
	[SerializeField]
	private int ID;
	[SerializeField]
	private ItemType itemType;
	[SerializeField]
	private Sprite itemSprite;

	private string name, description;

	public int CompareTo(Item other)
	{
		if (itemType != other.itemType)
			return itemType.CompareTo(other.itemType);

		return ID.CompareTo(other.ID);
	}

	public Sprite GetItemSprite()
	{
		return itemSprite;
	}
}
