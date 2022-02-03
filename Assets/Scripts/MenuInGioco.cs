using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGioco : MonoBehaviour
{

    static public bool GiocoInPausa = false;

    public GameObject PauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
        Time.timeScale = 1f;
        GiocoInPausa = false;
    }

    void Pause()
    {
        PauseMenuUI.SetActive(true);
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
