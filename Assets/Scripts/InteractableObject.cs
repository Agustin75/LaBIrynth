using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
	[SerializeField]
	private InteractionStep[] interactionSteps;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public InteractionStep[] GetSteps()
	{
		return interactionSteps;
	}
}
