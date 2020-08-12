using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : InteractableObject
{
	[SerializeField]
	private List<GameObject> obstacleSprites;

	[SerializeField]
	private BoolVariable upgradeRequired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override bool CanInteract()
	{
		return upgradeRequired;
	}

	public override InteractableObjectTypes GetObjectType()
	{
		return InteractableObjectTypes.Obstacle;
	}
}
