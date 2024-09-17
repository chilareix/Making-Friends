using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	//Loads the game scene
	public void StartGame() {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);}
	//.. exits .. the game? Directions' on the tin
	public void ExitGame() {Application.Quit();}
	public void TitleLabelChange(string nextLabel)
	{
		TMP_Text label = GameObject.FindGameObjectWithTag("Main Menu Label").GetComponent<TMP_Text>();
		label.text = nextLabel;
	}
}
