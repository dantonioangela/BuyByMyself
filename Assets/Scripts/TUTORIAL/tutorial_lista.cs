using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorial_lista : MonoBehaviour
{
    static public bool ListaAttiva = false;
    private bool tutorialStepDone = false;

    public GameObject ListaUI;

    public TextMeshProUGUI TestoLista;
    public tutorial_canvas_controller speech;

    private void Start()
    {
        InizializzaLista();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (ListaAttiva)
            {
                Resume();
            }
            else
            {
                if (!tutorialStepDone)
                {
                    speech.ChangeSpeech(2);
                    tutorialStepDone = true;
                }
                ShowLista();
                
            }
        }
    }
    void Resume()
    {
        ListaUI.SetActive(false);
        ListaAttiva = false;
    }

    void ShowLista()
    {
        ListaUI.SetActive(true);
        ListaAttiva = true;
    }

    void InizializzaLista()
    {
        string s = "";

        s = s + "due caschi di banane" + "\n\n" + "una bibita"  + "\n\n" + "una confezione di salmone" + "\n"  + "\n" + "\n" + "\n" + "\n" + "\n" + "PORTAFOGLIO : ? 7,00";

        TestoLista.text = s;
    }
}
