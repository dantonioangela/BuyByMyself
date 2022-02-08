using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuPrincipale : MonoBehaviour
{
    public Player_Controller player;
    public GameObject OptionsUI;
    public GameObject MainMenuUI;
    public AudioMixer audioMixer;

    static public bool MainMenuActive = false;

    Resolution[] resolutions;
    public Dropdown MAINResolutionDropdownUI;

    // Start is called before the first frame update
    void Start()
    {
        player.UI_active = true;
        MainMenuUI.SetActive(true);
        MainMenuActive = true;

        resolutions = Screen.resolutions;
        MAINResolutionDropdownUI.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }
        MAINResolutionDropdownUI.AddOptions(options);
        MAINResolutionDropdownUI.value = currentResolutionIndex;
        MAINResolutionDropdownUI.RefreshShownValue();
    }

    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        player.UI_active = false;
        MainMenuActive = false;
    }

    public void Opzioni()
    {
        MainMenuUI.SetActive(false);
        OptionsUI.SetActive(true);
    }

    public void Esci()
    {
        Debug.Log("Uscita applicazione...");
        Application.Quit();
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void Indietro()
    {
        OptionsUI.SetActive(false);
        MainMenuUI.SetActive(true);
    }

    public void Credits()
    {
        Debug.Log("Credits...");
    }

    public void Tutorial()
    {
        Debug.Log("Tutorial...");
    }

    public void SetRisoluzione(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
