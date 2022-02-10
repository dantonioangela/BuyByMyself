using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_UI : MonoBehaviour
{

    public tutorial_player_controller player_controller;
    public tutorial_cassiera cassiera_controller;
    private Transform UI_inventario;
    private Transform UI_cassiera_pay;
    private Transform UI_cassiera_notPay;
    private bool inventarioActive = false;
    //private GameObject[] productsInInventario = new GameObject[15];
    private List<GameObject> productsInInventario = new List<GameObject>();
    private int counter = 0;
    private int i = 0;



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
            if (!inventarioActive)
            {
                UI_inventario.gameObject.SetActive(true);
                inventarioActive = true;
                for (i = 0; i < counter; i++)
                {
                    transform.GetChild(0).GetChild(3).GetComponent<tutorial_inventario>().AddProduct(productsInInventario[i]);
                }
            }
        }
        else if (inventarioActive)
        {
            UI_inventario.gameObject.SetActive(false);
            inventarioActive = false;
        }
        if (cassiera_controller.isTalking && tutorial_carrello_controller.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(true);
        }
        else if (!cassiera_controller.isTalking && tutorial_carrello_controller.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(false);
        }
        else if (cassiera_controller.isTalking && tutorial_carrello_controller.mode == 1)
        {
            UI_cassiera_notPay.gameObject.SetActive(true);
        }
        else
        {
            UI_cassiera_notPay.gameObject.SetActive(false);
        }
    }


    public void AddProductToInventario(GameObject product)
    {

        if (counter < 14)
        {
            productsInInventario.Add(product);
            counter++;
        }
        else
        {
            Debug.Log("Inventario pieno");
        }
    }

    public void RemoveProductFromInventario(GameObject product)
    {
        productsInInventario.Remove(product);
        counter--;
    }
}
