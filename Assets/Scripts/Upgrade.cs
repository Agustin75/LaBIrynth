using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
	[Header("Scriptable Objects")]
	[SerializeField]
	private UpgradeInfo baseInfo;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public int GetID()
	{
		return baseInfo.GetID();
	}
}
