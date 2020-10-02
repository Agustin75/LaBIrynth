using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Upgrades")]
public class UpgradesManager : ScriptableObject
{
	[SerializeField]
	private List<BoolVariable> upgradesStatus;

	[Header("Scriptable Objects")]
	[SerializeField]
	private SaveManager saveManager;

	private bool upgradeObtained = false;
	private int upgradesStates = 0;

	public void Initialize()
	{
		if (saveManager.IsNewGame())
		{
			// NOTE: Should only need to do this once, as the other upgrades can just be added onto the int
			for (int i = 0; i < upgradesStatus.Count; i++)
			{
				if (upgradesStatus[i])
				{
					upgradesStates |= (1 << i);
				}
			}

			SaveInformation();
		}
		else
		{
			// Load the upgrade's states from file
			upgradesStates = saveManager.GetUpgradesInfo();
			for (int i = 0; i < upgradesStatus.Count; i++)
			{
				upgradesStatus[i].value = (upgradesStates & (1 << i)) != 0;
			}

			// Player has an Upgrade already
			upgradeObtained = upgradesStates != 0;
		}
	}

	public void UpgradeObtained(int _id)
	{
		if (_id < 0 || _id >= upgradesStatus.Count)
			return;
		upgradesStatus[_id].value = true;
		upgradeObtained = true;
		upgradesStates |= (1 << _id);
	}

	public void UpgradesDisplayed()
	{
		upgradeObtained = false;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Save Information
	// -------------------------
	// Tells the SaveManager which Upgrades the player has obtained so far
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SaveInformation()
	{
		saveManager.SaveUpgradesInfo(upgradesStates);
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
