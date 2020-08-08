using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private GameObject mapObjectsParent;

	[SerializeField]
	private GameEvent mapUnlockedEvent;

	private List<Map> maps;

    // Start is called before the first frame update
    void Start()
    {
		maps = new List<Map>(mapObjectsParent.GetComponentsInChildren<Map>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void UnlockMap(int _mapId)
	{
		if (_mapId < 0 || _mapId >= maps.Count)
			return;

		maps[_mapId].MapUnlocked();

		mapUnlockedEvent.Raise();
	}

	// TODO: Save all unlocked Maps

	// TODO: Unlock all saved Maps
}
