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
        //itemsNumber = 15;
        itemsNumber = 7;
        CreateList();
        UpdateProductModelsCounter();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateList(){
        int index;
        string productName;
        int quantity;

        index = Random.Range(0, Loader.modelsAvailability.Count);
        productName = Loader.modelsAvailability.ElementAt(index).Key;
        quantity = Random.Range(1, (int)(Loader.modelsAvailability[productName][1]/ 2));
        listaSpesa.Add(productName, quantity);
        for (int i = 1; i < itemsNumber; i++) {
            while (listaSpesa.ContainsKey(productName)) { 
                index = Random.Range(0, Loader.modelsAvailability.Count);
                productName = Loader.modelsAvailability.ElementAt(index).Key;
            }
            quantity = Random.Range(1, (int)(Loader.modelsAvailability[productName][1] / 2));
            listaSpesa.Add(productName, quantity);
        }

    }

    private void UpdateProductModelsCounter()
    {
        int index;
        int numProd;
        int numProdRestanti;
        int j;
        foreach( var i in Loader.modelsAvailability )
        {
            index = Loader.NamesToIndex[i.Key][0];  //indice prima occorrenza
            numProd = i.Value[1];
            numProdRestanti = numProd;
            if ( listaSpesa.ContainsKey(i.Key))
            {
                Loader.productModels[index].counter = listaSpesa[i.Key];
                numProdRestanti = numProd - listaSpesa[i.Key];
            }

            for (j = 0; j < numProdRestanti; j++)
            {
                Loader.productModels[ Random.Range(index, index + Loader.NamesToIndex[i.Key][1]) ].counter++;
            }
        }
    }

}
