using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_controller : MonoBehaviour
{
    public Player_Controller player_controller;
    public Cassiera_controller cassiera_controller;
    private Transform UI_inventario;
    private Transform UI_cassiera_pay;
    private Transform UI_cassiera_notPay;



    // Start is called before the first frame update
    void Start()
    {
        UI_inventario = transform.GetChild(0);
        UI_cassiera_pay = transform.GetChild(1);
        UI_cassiera_notPay = transform.GetChild(2);
        UI_inventario.gameObject.SetActive(false);
        UI_cassiera_pay.gameObject.SetActive(false);
        UI_cassiera_notPay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player_controller.inventario)
        {
            UI_inventario.gameObject.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            UI_inventario.gameObject.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (cassiera_controller.isTalking && player_controller.carrello.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(true);
        }
        else if(!cassiera_controller.isTalking && player_controller.carrello.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(false);
        }
        else if(cassiera_controller.isTalking && player_controller.carrello.mode == 1)
        {
            UI_cassiera_notPay.gameObject.SetActive(true);
        }
        else
        {
            UI_cassiera_notPay.gameObject.SetActive(false);
        }
    }
}
