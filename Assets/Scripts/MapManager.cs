using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private GameEvent mapUnlockedEvent;

	[SerializeField]
	// TODO: Change this to be more modular (Automatically get all relevant maps) and to allow multiple floors.
	private List<GameObject> maps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UnlockMap(int _mapId)
	{
		if (_mapId < 0 || _mapId >= maps.Count)
			return;

		// Hide the locked map
		maps[_mapId].SetActive(false);

		mapUnlockedEvent.Raise();
	}
}
