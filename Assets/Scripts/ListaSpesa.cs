using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Linq;

public class ListaSpesa : MonoBehaviour
{

    [System.NonSerialized] public static Dictionary<string, int> listaSpesa;
    private int itemsNumber;
    public int season;
    public static float budget = 0.0f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartMe()
    {
        listaSpesa = new Dictionary<string, int>();
        //itemsNumber = 15;
        itemsNumber = 7;
        CreateList();
        CalculateBudget();
        UpdateProductModelsCounter();
        season = Random.Range(0, 3);
        foreach(var i in listaSpesa)
        {
            Debug.Log("lista spesa: " + i.Key + i.Value);
        }
    }

    void CreateList(){
        int index;
        string productName;
        string productNameList;
        int quantity;
        if (listaSpesa.Count > 0)
        {
            listaSpesa.Clear();
        }

        itemsNumber = Random.Range((int)(itemsNumber * 0.7), itemsNumber + 1);

        for (int i = 0; i < itemsNumber; i++) {
            index = Random.Range(0, Loader.modelsAvailability.Count);
            productName = Loader.modelsAvailability.ElementAt(index).Key;
            productNameList = productName.Split('/')[0];
            while (listaSpesa.ContainsKey(productNameList)) { 
                index = Random.Range(0, Loader.modelsAvailability.Count);
                productName = Loader.modelsAvailability.ElementAt(index).Key;
                productNameList = productName.Split('/')[0];
            }
            quantity = Loader.modelsAvailability[productName][0];
            while (quantity == 0)
            {
                index = Random.Range(0, Loader.modelsAvailability.Count);
                productName = Loader.modelsAvailability.ElementAt(index).Key;
                productNameList = productName.Split('/')[0];
                quantity = Loader.modelsAvailability[productName][0];
            }
            quantity = Random.Range(1, (int)(quantity * 0.8));
            listaSpesa.Add(productNameList, quantity);
        }

    }

    private void UpdateProductModelsCounter()
    {
        int index;
        int numProd;
        int numProdRestanti;
        int j;
        string prodListName;
        foreach( var i in Loader.modelsAvailability )       
        {
            index = Loader.NamesToIndex[i.Key][0];  //indice prima occorrenza
            prodListName = i.Key.Split('/')[0];
            numProd = i.Value[1];
            numProdRestanti = numProd;          
            if ( listaSpesa.ContainsKey(prodListName) && i.Value[0] !=0)     
            {
                Loader.productModels[index].counter = listaSpesa[prodListName];
                numProdRestanti = numProd - listaSpesa[prodListName];
            }

            for (j = 0; j < numProdRestanti; j++)
            {
                Loader.productModels[ Random.Range(index, index + Loader.NamesToIndex[i.Key][1]) ].counter++;
            }
        }
    }

    private void CalculateBudget()
    {
        budget = 10f;
    }

}
