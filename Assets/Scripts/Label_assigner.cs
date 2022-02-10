using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Label_assigner : MonoBehaviour
{
    public string listName;
    private Transform[] child;
    private bool[] hasLabel;
    int i, j;
    int first_occurrance_index;
    int index_child;


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
        listName = listName.ToLower();
        if (!Loader.NamesToIndex.ContainsKey(listName))
        {
            Debug.LogError("LABEL_ASSIGNER: " + listName + "non � contenuto nella NamesToIndex!");
        }
        first_occurrance_index = Loader.NamesToIndex[listName][0];

        hasLabel = new bool[transform.childCount];
        for (i = 0; i < transform.childCount; i++)
        {
            hasLabel[i] = false;
        }
        child = new Transform[transform.childCount];
        for (i = 0; i < transform.childCount; i++)
        {
            child[i] = transform.GetChild(i);
        }

        for (i = 0; i < transform.childCount; i++)                          //per Num_tot_figli volte
        {
            index_child = Random.Range(0, transform.childCount);        //prendo un figlio casuale
            while (hasLabel[index_child])                                   //senza etichetta
            {
                index_child = Random.Range(0, transform.childCount);
            }
            hasLabel[index_child] = true;


            j = first_occurrance_index;
            while (Loader.productModels[j].counter <= 0)
            {
                j++;
                if (j == first_occurrance_index + Loader.NamesToIndex[listName][1] - 1) break;
            }
            Loader.productModels[j].counter--;
            //assegnare tutto al figlio index_child
            child[index_child].gameObject.AddComponent<Product>();
            child[index_child].gameObject.GetComponent<Product>().model = Loader.productModels[j];
            if (ListaSpesa.listaSpesa.ContainsKey(listName))
            {
                if (j == first_occurrance_index && Loader.productModels[j].counter <= ListaSpesa.listaSpesa[listName])
                {
                    child[index_child].gameObject.GetComponent<Product>().expirationDate = 0;
                }
                else
                {
                    child[index_child].gameObject.GetComponent<Product>().expirationDate = Random.Range(0, 2);
                }
            }
            else
            {
                child[index_child].gameObject.GetComponent<Product>().expirationDate = Random.Range(0, 2);
            }

        }

        foreach (var i in child)
        {
            Debug.Log(i.GetComponent<Product>().model.listName + ", " + i.GetComponent<Product>().model.name + ":: " + "counter = " + i.GetComponent<Product>().model.counter);
        }

    }
}
