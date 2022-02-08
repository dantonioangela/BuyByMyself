using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Linq;

public class ListaSpesa : MonoBehaviour
{

    public Dictionary<string, int> listaSpesa;
    private int itemsNumber;

    // Start is called before the first frame update
    void Start()
    {
        listaSpesa = new Dictionary<string, int>();
        itemsNumber = 15;
        createList();
        foreach(var entry in listaSpesa){
            Debug.Log("Prodotto: " + entry.Key + " Quantità: " + entry.Value);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void createList(){
        int index;
        string productName;
        int quantity;
        //TODO: controllare di non mettere nella lista prodotti marci facendo check su nome (id?). 
        //TODO: mettere nome del prodotto nella lista invece che tag
        index = Random.Range(0, Loader.modelsAvailability.Count);
        productName = Loader.modelsAvailability.ElementAt(index).Key;
        quantity = Random.Range(1, Loader.modelsAvailability[productName] - (int)(Loader.modelsAvailability[productName] / 2));
        listaSpesa.Add(productName, quantity);
        for (int i = 1; i < itemsNumber; i++) {
            while (listaSpesa.ContainsKey(productName)) { 
                index = Random.Range(0, Loader.modelsAvailability.Count);
                productName = Loader.modelsAvailability.ElementAt(index).Key;
            }
            //string[] productNames = new string[Loader.modelsAvailability.Count];
            quantity = Random.Range(1, Loader.modelsAvailability[productName] - (int)(Loader.modelsAvailability[productName]/2));
            listaSpesa.Add(productName, quantity);
        }

    }

}
