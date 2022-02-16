using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mappa : MonoBehaviour
{
    static public bool MappaAttiva = false;

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
            }
        }
    }
    public void Resume()
    {
        MappaUI.SetActive(false);
        MappaAttiva = false;
    }

    void ShowMappa()
    {
        MappaUI.SetActive(true);
        MappaAttiva=true;
    }
}
