using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
	// Tiles to be displayed while the map is locked only
	[SerializeField]
	private GameObject lockedMapGrid;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void MapUnlocked()
	{
		// TODO: Will have to add an animation or feedback of some sort when a map is unlocked

		// Hide the "Locked Map" tiles
		lockedMapGrid.SetActive(false);
	}
}
