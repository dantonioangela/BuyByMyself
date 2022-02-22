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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Start()
    {
        listName = listName.ToLower();
        if (!Loader.NamesToIndex.ContainsKey(listName))
        {
            Debug.LogError("LABEL_ASSIGNER: " + listName + " non è contenuto nella NamesToIndex!");
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
                if (j == first_occurrance_index + Loader.NamesToIndex[listName][1]) break;// - 1) break;
            }

            //assegnare tutto al figlio index_child
            child[index_child].gameObject.AddComponent<Product>();
            child[index_child].gameObject.GetComponent<Product>().model = Loader.productModels[j];

            if (ListaSpesa.listaSpesa.ContainsKey(listName.Split('/')[0]))
            {
                if (j == first_occurrance_index && Loader.productModels[j].counter <= ListaSpesa.listaSpesa[listName.Split('/')[0]])
                {
                    child[index_child].gameObject.GetComponent<Product>().expirated = false;
                    FindObjectOfType<icons_listaspesa>().addIcon(listName.Split('/')[0], child[index_child].gameObject.GetComponent<icon>().myIcon);
                }
                else
                {
                    child[index_child].gameObject.GetComponent<Product>().expirated = (int)Random.Range(0, 2) == 0 ? true : false;
                }
            }
            else
            {
                child[index_child].gameObject.GetComponent<Product>().expirated = (int)Random.Range(0, 2) == 0 ? true : false;
            }
            Loader.productModels[j].counter--;

        }
    }
}
