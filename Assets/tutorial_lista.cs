using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        foreach (var i in ListaSpesa.listaSpesa)
        {
            //stampi i.Key e i.Value
            s = s + i.Value + "  " + i.Key + "\n";
        }
        TestoLista.text = s;
    }
}
