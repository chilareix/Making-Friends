using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	//Loads the game scene
	public void StartGame() {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
	//.. exits .. the game? Directions' on the tin
	public void ExitGame()
	{
		Application.Quit();
	}
}
