using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_mappa : MonoBehaviour
{
    static public bool MappaAttiva = false;
    public tutorial_canvas_controller speech;

    public GameObject MappaUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MappaAttiva)
            {
                Resume();
            }
            else
            {
                ShowMappa();
                speech.ChangeSpeech(2);
            }
        }
    }
    void Resume()
    {
        MappaUI.SetActive(false);
        MappaAttiva = false;
    }

    void ShowMappa()
    {
        MappaUI.SetActive(true);
        MappaAttiva = true;
    }
}
