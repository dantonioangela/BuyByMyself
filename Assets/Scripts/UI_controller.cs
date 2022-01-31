using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_controller : MonoBehaviour
{
    public Player_Controller player_controller;
    private Transform UI_inventario;

    // Start is called before the first frame update
    void Start()
    {
        UI_inventario = transform.GetChild(0);
        UI_inventario.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player_controller.inventario)
        {
            UI_inventario.gameObject.SetActive(true);
        }
        else
        {
            UI_inventario.gameObject.SetActive(false);
        }
    }
}
