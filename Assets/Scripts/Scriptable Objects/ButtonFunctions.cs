using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class ButtonFunctions : ScriptableObject
{
	public void OpenScene(string _sceneToOpen)
	{
		SceneManager.LoadScene(_sceneToOpen);
	}

	public void CloseGame()
	{
		Application.Quit();
	}
}
