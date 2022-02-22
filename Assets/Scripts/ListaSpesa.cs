using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using System.Linq;

public class ListaSpesa
{

    public static Dictionary<string, int> listaSpesa;
    private static int itemsNumber;
	public static float idealBudget;
    public static float budget;
    public static int season;
    public static bool setSeason = false;

    public static void InitList()
    {
        itemsNumber = 8;
        budget = 0.0f;
        listaSpesa = new Dictionary<string, int>();
        CreateList();
        CalculateBudgets();
        UpdateProductModelsCounter();
        season = Random.Range(0, 4);
        setSeason = true;
        //FindObjectOfType<Lista>().InizializzaLista();
    }

    private static void CreateList(){
        int index;
        string productName;
        string productNameList;
        int quantity;

        listaSpesa.Clear();

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
                while (listaSpesa.ContainsKey(productNameList))
                {
                    index = Random.Range(0, Loader.modelsAvailability.Count);
                    productName = Loader.modelsAvailability.ElementAt(index).Key;
                    productNameList = productName.Split('/')[0];
                }
                quantity = Loader.modelsAvailability[productName][0];
            }
            if(quantity > 4)
            {
                quantity = Random.Range(1, (int)(quantity*0.7f));
            }
            else
            {
                quantity = Random.Range(1, quantity);
            }
            if (quantity > 3) quantity = 3;
            listaSpesa.Add(productNameList, quantity);
            budget += quantity*(Loader.productModels[ Loader.NamesToIndex[productName][0]].price);
        }

    }

    private static void UpdateProductModelsCounter()
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

    private static void CalculateBudgets()
    {
        idealBudget = budget;
        budget += 10f;
    }

}
