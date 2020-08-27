using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoDisplay : MonoBehaviour
{
	[SerializeField]
	private Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void SetItem(Item _item)
	{
		// TODO: Initialize the Info display
		itemImage.sprite = _item.GetItemSprite();
		itemImage.gameObject.SetActive(true);
	}

	public void ClearItemInfo()
	{
		// TODO: Remove all the previous Item's info
		itemImage.gameObject.SetActive(false);
		itemImage.sprite = null;
	}
}
