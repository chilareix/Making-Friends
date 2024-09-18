using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public GameObject _PauseMenu;
	public GameObject PauseSettings;
	public GameObject HUD;
	private bool IsPaused = false;

	/// <summary>
	/// Closes the game
	/// </summary>
	// TODO: Implement saving
    public void QuitGame() { Application.Quit(); }
	/// <summary>
	/// Returns player to main menu
	/// </summary>
	// TODO: Implement saving
	public void GoToMainMenu()
	{ 
		SceneManager.LoadScene(0); 
	}
	//Pauses and unpauses the game
	public void TogglePause()
	{
		if (IsPaused)
		{
			IsPaused = false;
			Time.timeScale = 0;
			_PauseMenu.SetActive(true);
			HUD.SetActive(false);
			return;
		}
		IsPaused = true;
		Time.timeScale = 1.0f;
		PauseSettings.SetActive(false);
		_PauseMenu.SetActive(false);
		HUD.SetActive(true);
	}
}
