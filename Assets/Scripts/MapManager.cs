using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
	[SerializeField]
	private List<Map> maps;

	[SerializeField]
	private GameEvent mapUnlockedEvent;

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

		maps[_mapId].MapUnlocked();

		mapUnlockedEvent.Raise();
	}

	// TODO: Save all unlocked Maps

	// TODO: Unlock all saved Maps
}
