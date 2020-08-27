using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class UpgradeInfo : ScriptableObject
{
	[SerializeField]
	private int id;
	[SerializeField]
	private Sprite upgradeSprite;

	private string name, description;

	public int GetID()
	{
		return id;
	}

	public Sprite GetSprite()
	{
		return upgradeSprite;
	}
}
