using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class tutorial_lista : MonoBehaviour
{
    static public bool ListaAttiva = false;

    public GameObject ListaUI;

    public TextMeshProUGUI TestoLista;

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

        s = s + "banane" + "  " + "10" + "\n";

        TestoLista.text = s;
    }
}
