using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Upgrades")]
public class UpgradesManager : ScriptableObject
{
	[SerializeField]
	private List<BoolVariable> upgradesStatus;

	private bool upgradeObtained = false;

	public void UpgradeObtained(int _id)
	{
		if (_id < 0 || _id >= upgradesStatus.Count)
			return;
		upgradesStatus[_id].value = true;
		upgradeObtained = true;
	}

	public void UpgradesDisplayed()
	{
		upgradeObtained = false;
	}

	public bool WasUpgradeObtained()
	{
		return upgradeObtained;
	}

	public List<BoolVariable> GetUpgradesStatus()
	{
		return upgradesStatus;
	}
}
