using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GatePopup : MonoBehaviour
{
	[SerializeField]
	// Item required to open this gate
	private Image itemImage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void ShowPopup(Item _item)
	{
		itemImage.sprite = _item.GetItemSprite();
	}
}
