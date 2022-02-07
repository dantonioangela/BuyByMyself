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

    // Start is called before the first frame update
    void Start()
    {
        player.UI_active = true;
        MainMenuUI.SetActive(true);
    }

    public void StartGame()
    {
        MainMenuUI.SetActive(false);
        player.UI_active = false;
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

    public void Difficolta()
    {
        Debug.Log("Selezione difficoltà");
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
}
