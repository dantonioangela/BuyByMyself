using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lista : MonoBehaviour
{
    static public bool ListaAttiva = false;

    public GameObject ListaUI;

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
        foreach(var i in ListaSpesa.listaSpesa)
        {
            //stampi i.Key e i.Value
        }
        ListaUI.SetActive(true);
        ListaAttiva = true;
    }
}
