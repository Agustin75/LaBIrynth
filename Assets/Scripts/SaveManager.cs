using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(menuName = "Managers/Save")]
public class SaveManager : ScriptableObject
{
	[System.Serializable]
	public struct SaveData
	{
		public FloorInfo[] floorsData;
		public int UpgradesObtained;
	}

	[System.Serializable]
	public struct FloorInfo
	{
		public int[] objectsStates;
	}

	private bool isNewGame;
	private SaveData gameData;
	// List used only before saving, will be converted into array to save to file
	private List<FloorInfo> floorsInformation;

	// Save data to file
	public void SaveFile()
	{
		// Convert Floor Info List into array
		gameData.floorsData = floorsInformation.ToArray();

		BinaryFormatter bf = new BinaryFormatter();

		if (!Directory.Exists(Application.persistentDataPath + "/saves"))
		{
			Directory.CreateDirectory(Application.persistentDataPath + "/saves");
		}

		string path = Application.persistentDataPath + "/saves/" + "save.dat";

		FileStream file = File.Create(path);

		bf.Serialize(file, gameData);

		file.Close();
	}

	// Load data from file
	// Return - true if the data was successfully loaded, false otherwise
	// TODO: Add _index parameter, if multiple save files are implemented
	public bool LoadFile()
	{
		// No saved file exists
		if (!File.Exists(Application.persistentDataPath + "/saves/save.dat"))
		{
			// Initialize the variables for a new save file
			isNewGame = true;

			gameData = new SaveData();

			floorsInformation = new List<FloorInfo>();

			return false;
		}

		BinaryFormatter bf = new BinaryFormatter();

		FileStream file = File.Open(Application.persistentDataPath + "/saves/save.dat", FileMode.Open);

		try
		{
			gameData = (SaveData)bf.Deserialize(file);
			// Convert Floor Info Data array to List for easier handling
			floorsInformation = new List<FloorInfo>(gameData.floorsData);
		}
		catch
		{
			// Error: Couldn't deserialize file
		}

		file.Close();

		isNewGame = false;
		return true;
	}

	// Returns the information for the specified floor
	// _index - Floor to return (Should be between 0 and floorsInformation.Length - 1
	public int GetFloorInfo(int _floorNumber, SaveDataType _infoType)
	{
		return floorsInformation[_floorNumber].objectsStates[(int)_infoType];
	}

	// Returns the information for the upgrades
	public int GetUpgradesInfo()
	{
		return gameData.UpgradesObtained;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set Floor Value
	// -------------------------
	// Updates the value of the objects in the save data
	// -------------------------
	// _floorNumber - The floor this object is in (Should be a 0-base index)
	// _objectsStates - The state of all the objects in the floor
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SaveFloorObjects(int _floorNumber, int[] _objectsStates)
	{
		if (_floorNumber < 0)
		{
			// Error: Array wasn't initialized correctly, or Floor Number doesn't exist
			return;
		}

		// Temporarily save the Floor number
		int currFloorNum = _floorNumber;
		// While the Floor number is higher than the last floor's number
		while (floorsInformation.Count - 1 < currFloorNum)
		{
			// Add an empty floor to the List
			floorsInformation.Add(new FloorInfo());
		}

		// Create a temporary floor (Items inside a Struct in a List cannot be modified from outside the List)
		FloorInfo temp = new FloorInfo();
		// Save the floor's objects' states in the temporary floor
		temp.objectsStates = _objectsStates;
		// Replace the floor saved with the new floor's data in the List
		floorsInformation[_floorNumber] = temp;
	}

	public void SaveUpgradesInfo(int _upgradesStates)
	{
		gameData.UpgradesObtained = _upgradesStates;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Delete Saved Data
	// -------------------------
	// Deletes every saved information in the device
	// NOTE: Make sure the player knows that this is irrevocable
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void DeleteSavedData()
	{
		if (File.Exists(Application.persistentDataPath + "/saves/save.dat"))
		{
			File.Delete(Application.persistentDataPath + "/saves/save.dat");
		}
	}

	////////////////////////////////////////
	// Accessors
	////////////////////////////////////////
	public bool IsNewGame()
	{
		return isNewGame;
	}

	public bool DoesSaveFileExist()
	{
		return File.Exists(Application.persistentDataPath + "/saves/save.dat");
	}
}
