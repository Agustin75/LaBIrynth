using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorManager : MonoBehaviour
{
	[SerializeField]
	// Floor number should be a number starting at 1 and never repeating
	private int floorNumber;

	[SerializeField]
	private GameObject enemiesParent;
	[SerializeField]
	private GameObject gatesParent;
	[SerializeField]
	private GameObject mapsParent;
	[SerializeField]
	private GameObject obstaclesParent;
	[SerializeField]
	private GameObject runesParent;
	[SerializeField]
	private GameObject upgradesParent;
	[SerializeField]
	private GameObject warpsParent;

	[Header("Scriptable Objects")]
	[SerializeField]
	private SaveManager saveManager;

	private List<SaveObject> enemies;
	private List<SaveObject> gates;
	private List<SaveObject> maps;
	private List<SaveObject> obstacles;
	private List<SaveObject> runes;
	private List<SaveObject> upgrades;
	private List<SaveObject> warps;

	private int[] objectsStates;

	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Initialize()
	{
		objectsStates = new int[(int)SaveDataType.NumTypes];

		// Populate the GameObject Lists
		SetUpObjectList(enemiesParent, ref enemies);
		SetUpObjectList(gatesParent, ref gates);
		SetUpObjectList(mapsParent, ref maps);
		SetUpObjectList(obstaclesParent, ref obstacles);
		SetUpObjectList(runesParent, ref runes);
		SetUpObjectList(upgradesParent, ref upgrades);
		SetUpObjectList(warpsParent, ref warps);

		// If the player started a new game
		if (saveManager.IsNewGame())
		{
			// Add the objects default values to the saved data
			SaveInformation();

			return;
		}

		// Initialize all the objects to their saved values
		// floorNumber - 1 to account for 0-based index
		InitializeObjectList(ref enemies, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Enemies), SaveDataType.Enemies);
		InitializeObjectList(ref gates, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Gates), SaveDataType.Gates);
		InitializeObjectList(ref maps, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Maps), SaveDataType.Maps);
		InitializeObjectList(ref obstacles, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Obstacles), SaveDataType.Obstacles);
		InitializeObjectList(ref runes, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Runes), SaveDataType.Runes);
		InitializeObjectList(ref upgrades, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Upgrades), SaveDataType.Upgrades);
		InitializeObjectList(ref warps, saveManager.GetFloorInfo(floorNumber - 1, SaveDataType.Warps), SaveDataType.Warps);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Save Information
	// -------------------------
	// Saves the current state of all the objects in the scene to be saved to file
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SaveInformation()
	{
		SaveObjectsStates(ref enemies, SaveDataType.Enemies);
		SaveObjectsStates(ref gates, SaveDataType.Gates);
		SaveObjectsStates(ref maps, SaveDataType.Maps);
		SaveObjectsStates(ref obstacles, SaveDataType.Obstacles);
		SaveObjectsStates(ref runes, SaveDataType.Runes);
		SaveObjectsStates(ref upgrades, SaveDataType.Upgrades);
		SaveObjectsStates(ref warps, SaveDataType.Warps);

		// floorNumber - 1 to account for 0-based index
		saveManager.SaveFloorObjects(floorNumber - 1, objectsStates);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// HELPERS
	//////////////////////////////////////////////////////////////////////////////////////////////////////

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set Up Object List
	// -------------------------
	// Grabs all the children whose information is saved from the parent passed in
	// -------------------------
	// _listParent - Parent to check
	// _list - List to add the appropriate children to
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	private void SetUpObjectList(GameObject _listParent, ref List<SaveObject> _list)
	{
		// NOTE: Old code, used to grab only the children when no special SaveObject class exists
		//foreach (Transform item in _listParent.GetComponentsInChildren<Transform>(true))
		//{
		//	// If it's not an immediate child of the parent
		//	if (item.parent != _listParent.transform)
		//		continue;
		//	_list.Add(item.gameObject);
		//}

		_list = new List<SaveObject>(_listParent.GetComponentsInChildren<SaveObject>(true));
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Initialize Object List
	// -------------------------
	// Initializes the List of GameObjects passed in based on _objectValues
	// -------------------------
	// _objectList - Objects to initialize
	// _objectValues - Values to give each object
	// _type - Type of object being initialized
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	private void InitializeObjectList(ref List<SaveObject> _objectList, int _objectValues, SaveDataType _type)
	{
		// Save the number of objects
		int size = _objectList.Count;

		if (size > sizeof(int) * 8)
		{
			// Error: The list has more objects than the amount allowed to be saved in an int
			Debug.Log("Warning: Too many objects of the same type on the same floor");
			return;
		}

		objectsStates[(int)_type] = _objectValues;

		// Loop through all objects
		for (int i = 0; i < size; i++)
		{
			// Set the object status to the corresponding value - Set the object's state based on whether the bit i of the array is on
			_objectList[i].SetObjectState((objectsStates[(int)_type] & (1 << i)) != 0);
		}
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Save Objects States
	// -------------------------
	// Updates objectsStates to the current state of every SaveObject in the passed list
	// -------------------------
	// _objectList - Object states to save
	// _type - Type of object save
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	private void SaveObjectsStates(ref List<SaveObject> _objectList, SaveDataType _type)
	{
		// Loop through all the objects on the list
		for (int i = 0; i < _objectList.Count; i++)
		{
			// If the current object's state is true
			if (_objectList[i].GetCurrentState())
				// Save it in objectStates
				objectsStates[(int)_type] |= (1 << i);
		}
	}
}
