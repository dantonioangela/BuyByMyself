using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_mappa : MonoBehaviour
{
    static public bool MappaAttiva = false;
    public tutorial_canvas_controller speech;
    private bool tutorialStepDone = false;

    public GameObject banana1;
    public GameObject banana2;

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
                if (!tutorialStepDone)
                {
                    speech.ChangeSpeech(4);
                    banana1.GetComponent<MeshCollider>().enabled = true;
                    banana2.GetComponent<MeshCollider>().enabled = true;
                    banana1.GetComponent<tutorial_product>().enabled = true;
                    banana2.GetComponent<tutorial_product>().enabled = true;

                    tutorialStepDone = true;
                }
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
