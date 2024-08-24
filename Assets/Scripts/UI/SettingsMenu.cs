using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
	public AudioMixer Mixer;
	public TMP_Dropdown ResolutionDropdown;
	Resolution[] Resolutions;

	private void Start()
	{
		Resolutions = Screen.resolutions; //Gets a list of available resolutions
		ResolutionDropdown.ClearOptions(); //Clears the dropdown of placeholder resolutions
		List<string> ResolutionOptions = new List<string>(); //Creates a list object of type string to store the resolutions
		int currentResolution = 0; //Gets your default resolution
		//Loops through resolutions
		for (int i = 0; i < Resolutions.Length; i++)
		{
			ResolutionOptions.Add(Resolutions[i].width + "x" + Resolutions[i].height);//Adds resolutions to string list object
			//Sets default option to your default resolution
			if (Resolutions[i].width == Screen.currentResolution.width&&
				Resolutions[i].height == Screen.currentResolution.height)
			{
				currentResolution = i;
			}
		}

		ResolutionDropdown.AddOptions(ResolutionOptions); //Adds resolutions to dropdown
		ResolutionDropdown.value = currentResolution; //Sets default resolution
		ResolutionDropdown.RefreshShownValue(); //Refreshes the dropdown menu
	}
	//Sets the volume to the master mixer based on what the slider is at
	public void SetVolume(float vol) {Mixer.SetFloat("Master", vol);}
	//Sets the graphics quality from the dropdown
	public void SetQuality(int qualityIndex)
	{
		print(qualityIndex);
		QualitySettings.SetQualityLevel(qualityIndex);
	}
	//Sets fullscreen mode based on the toggle button
	public void ToggleFullScreen (bool isFullScreen) {Screen.fullScreen = isFullScreen;}
	//Sets the resolution based on the dropdown
	public void SetResolution(int resolutionIndex)
	{
		Resolution resolution = Screen.resolutions[resolutionIndex];
		Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
	}
}
