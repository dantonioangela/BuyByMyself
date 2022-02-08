using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuInGioco : MonoBehaviour
{

    static public bool GiocoInPausa = false;
    public Player_Controller player;

    public GameObject PauseMenuUI;
    public GameObject OptionsMenuUI;
    public GameObject MainMenuUI;

    public AudioMixer audioMixer;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MenuPrincipale.MainMenuActive)
            {
                if (GiocoInPausa)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }
    }
    public void Resume()
    {
        PauseMenuUI.SetActive(false);
        player.UI_active = false;
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
        player.UI_active = true;
        Time.timeScale = 0f;
        GiocoInPausa = true;
    }

    public void Opzioni()
    {
        PauseMenuUI.SetActive(false);
        OptionsMenuUI.SetActive(true);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        PauseMenuUI.SetActive(false);
        MainMenuUI.SetActive(true);
        MenuPrincipale.MainMenuActive = true;
    }

    public void Indietro()
    {
        OptionsMenuUI.SetActive(false);
        PauseMenuUI.SetActive(true);
    }

    public void Volume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

}
