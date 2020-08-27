using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeInfoDisplay : MonoBehaviour
{
	[SerializeField]
	private Image upgradeImage;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void SetUpgrade(UpgradeInfo _upgradeInfo)
	{
		// TODO: Initialize the Info display
		upgradeImage.sprite = _upgradeInfo.GetSprite();
		upgradeImage.gameObject.SetActive(true);
	}
}
