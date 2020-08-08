using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
	// TODO: Temporary code, remove and replace for correct implementation for final game
	[SerializeField]
	private GameObject gameOverPanel;

	[Header("Events")]
	[SerializeField]
	private GameEvent stairsTaken;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void TakeStairs()
	{
		gameOverPanel.SetActive(true);

		stairsTaken.Raise();
	}
}
