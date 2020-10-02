using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	//////////////////////////////////////////////////////////////////////////////////////////////////////
	// Set Object State
	// -------------------------
	// Sets the current status of the object (Called when loading a game)
	// -------------------------
	// Note: By default, this controls whether the GameObject is active
	// Note: A state of true means the GameObject will be inactive, cause it fulfilled its purpose
	//////////////////////////////////////////////////////////////////////////////////////////////////////
	public virtual void SetObjectState(bool _state)
	{
		gameObject.SetActive(!_state);
	}

	public virtual bool GetCurrentState()
	{
		return !gameObject.activeSelf;
	}
}
