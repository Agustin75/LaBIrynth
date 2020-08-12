using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
	[Header("Scriptable Objects")]
	[SerializeField]
	private BoolVariable upgradeUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void Obtained()
	{
		// TODO: Show animation/message/feedback for obtaining the upgrade
		// TODO: Play obtained sound

		upgradeUnlocked.value = true;

		Destroy(gameObject);
	}
}
