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
    private bool inventarioActive = false;
    //private GameObject[] productsInInventario = new GameObject[15];
    private List<Product> productsInInventario = new List<Product>();
    private List<Product> productsInInventarioNextPage = new List<Product>();
    private int counter = 0;
    private int counterNextPage = 0;
    private int totSlotPerPage = 15;
    private Product productReplacement;
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
        if (player_controller.inventario )
        {
            if (!inventarioActive)
            {
                UI_inventario.gameObject.SetActive(true);
                transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
                transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                inventarioActive = true;
               /*if (counter < totSlotPerPage)
                {
                    for (i = 0; i < counter; i++)
                    {
                        transform.GetChild(0).GetChild(4).GetComponent<inventario_manager>().AddProduct(productsInInventario[i]);
                    }
                }
                else
                {*/
                    for (i = 0; i < productsInInventario.Count; i++)
                    {
                        transform.GetChild(0).GetChild(4).GetComponent<inventario_manager>().AddProduct(productsInInventario[i]);
                    }
                    for(i = 0; i < productsInInventarioNextPage.Count; i++)
                    {
                        transform.GetChild(0).GetChild(5).GetComponent<inventario_manager>().AddProduct(productsInInventarioNextPage[i]);
                    }
                //}
                transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
                transform.GetChild(0).GetChild(5).gameObject.SetActive(false);

            }
        }
        else if(inventarioActive)
        {
            UI_inventario.gameObject.SetActive(false);
            inventarioActive = false;
        }
        if (cassiera_controller.isTalking && Carrello_controller.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(true);
        }
        else if(!cassiera_controller.isTalking && Carrello_controller.mode == 0)
        {
            UI_cassiera_pay.gameObject.SetActive(false);
        }
        else if(cassiera_controller.isTalking && Carrello_controller.mode == 1)
        {
            UI_cassiera_notPay.gameObject.SetActive(true);
        }
        else
        {
            UI_cassiera_notPay.gameObject.SetActive(false);
        }
    }


    public void AddProductToInventario(Product product)
    {
        
        if (productsInInventario.Count < 15)
        {
            productsInInventario.Add(product);
            counter++;
        }
        else if (productsInInventarioNextPage.Count < 15)
        {
            productsInInventarioNextPage.Add(product);
            counter++;
        }
    }

    public void RemoveProductFromInventario (Product product)
    {
        if (productsInInventario.Contains(product))
        {
            productsInInventario.Remove(product);
            counter--;
            if (productsInInventarioNextPage.Count > 0)
            {
                productReplacement = productsInInventarioNextPage[productsInInventarioNextPage.Count - 1];
                productsInInventario.Add( productReplacement );
                productsInInventarioNextPage.Remove(productReplacement);
                counterNextPage--;
                transform.GetChild(0).GetChild(5).GetComponent<inventario_manager>().ReplaceProduct(productsInInventarioNextPage.Count, productReplacement );
                transform.GetChild(0).GetChild(4).GetComponent<inventario_manager>().AddProduct(productReplacement);

            }
        }
        else
        {
            productsInInventarioNextPage.Remove(product);
            counterNextPage--;
        }
        
    }
}
