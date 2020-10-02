using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	[SerializeField]
	private Button deleteDataButton;

	[Header("Scriptable Objects")]
	[SerializeField]
	private SaveManager saveManager;

    // Start is called before the first frame update
    void Start()
    {
		deleteDataButton.interactable = saveManager.DoesSaveFileExist();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public void DeleteSavedData()
	{
		saveManager.DeleteSavedData();
		deleteDataButton.interactable = false;
	}
}
