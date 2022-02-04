using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGioco : MonoBehaviour
{

    static public bool GiocoInPausa = false;
    public Player_Controller player;

    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
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
        Debug.Log("Opzioni...");
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        Debug.Log("Ritorno al MainMenu...");
    }

}
