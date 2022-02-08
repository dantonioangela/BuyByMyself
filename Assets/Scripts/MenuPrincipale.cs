using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuPrincipale : MonoBehaviour
{
    public Player_Controller player;
    public GameObject OptionsUI;
    public GameObject MainMenuUI;
    public AudioMixer audioMixer;

    static public bool MainMenuActive = false;

    // Start is called before the first frame update
    void Start()
    {
        player.UI_active = true;
        MainMenuUI.SetActive(true);
        MainMenuActive = true;
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
}
