using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class Resoconti : MonoBehaviour
{
    public GameObject ResocontoUI;
    public GameObject ScenaFacile;
    public GameObject ScenaMedia;
    public GameObject ScenaDifficile;

    public static bool ResocontoActive = false;

    public Animator LegendaAnim;
    public bool legendaOpened = false;

    public Animator OggettiNonInListaAnim;
    public bool oggettiNonInListaOpened = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ResocontoActive && ResocontoUI.activeSelf==false)
        {
            ResocontoUI.SetActive(true);
            CreateResoconto();
        }
        
    }

    void CreateResoconto()
    {
        string productShoppingListName;
        Transform productInTable;
        Transform extraProductsList = ResocontoUI.transform.GetChild(5).GetChild(0).GetChild(1).GetChild(0);
        int extraProductCounter = 0;
        Transform slot;

        for (int i = 0; i < ListaSpesa.listaSpesa.Count; i++)
        {
            int productCounter = 0;
            productInTable = ResocontoUI.transform.GetChild(3).GetChild(i);

            foreach (var product in Carrello_controller.prodottiNelCarrello)
            {
                productShoppingListName = product.model.listName.Split('/')[0].ToLower();

                if (ListaSpesa.listaSpesa.ElementAt(i).Key.Equals(productShoppingListName) &&
                    productCounter < ListaSpesa.listaSpesa.ElementAt(i).Value)
                {
                    slot = productInTable.GetChild(productCounter);

                    if (productCounter == 0)
                    {
                        productInTable.GetComponent<RawImage>().texture = product.GetComponent<icon>().myIcon;
                        productInTable.GetComponent<RawImage>().enabled = true;
                    }

                    //controllo scadenza

                    if (!product.expirated)
                        slot.Find("Scadenza0_" + productCounter).GetComponent<RawImage>().enabled = true;
                    else
                        slot.Find("Scadenza0_0BAD").GetComponent<RawImage>().enabled = true;

                    //controllo qualità
                    if (product.model.listName.Contains("frutta") ||
                        product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualità
                    {
                        if (!product.model.listName.Contains("_old")) //se non è marcio
                            slot.Find("Qualità0_" + productCounter).GetComponent<RawImage>().enabled = true;
                        else
                            slot.Find("Qualità0_0BAD").GetComponent<RawImage>().enabled = true;
                    }

                    //controllo eco

                    if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                    {
                        if (product.model.packaging.Value == true) //se è stata presa la versione eco aggiungo punti
                            slot.Find("Eco0_" + productCounter).GetComponent<RawImage>().enabled = true;
                        else
                            slot.Find("Eco0_0BAD").GetComponent<RawImage>().enabled = true;
                    }

                    if(MenuPrincipale.levelDifficulty >= 1)
                    {
                        //controllo origine
                        if (product.model.origin.HasValue) //caso in cui il prodotto ha origine
                        {
                            if(product.model.origin == 1)
                                slot.Find("Provenienza0_" + productCounter).GetComponent<RawImage>().enabled = true;
                            else if(product.model.origin == 0.5f)
                                slot.Find("Provenienza0_0MIDDLE").GetComponent<RawImage>().enabled = true;
                            else
                                slot.Find("Provenienza0_0BAD").GetComponent<RawImage>().enabled = true;
                        }
                    }
                    if(MenuPrincipale.levelDifficulty == 2)
                    {
                        //controllo sostenibilità
                        if (product.model.sustainable.HasValue) //caso in cui il prodotto ha versione sostenibile
                        {
                            if (product.model.sustainable.Value == true)
                                slot.Find("Sostenibilità0_" + productCounter).GetComponent<RawImage>().enabled = true;
                            else
                                slot.Find("Sostenibilità0_0BAD").GetComponent<RawImage>().enabled = true;
                        }

                        //controllo stagione
                        if (product.model.season != null) //caso in cui il prodotto è stagionale
                        {
                            if (product.model.season[ListaSpesa.season] == 1)
                                slot.Find("Stagioni0_" + productCounter).GetComponent<RawImage>().enabled = true;
                            else
                                slot.Find("Stagioni0_0BAD").GetComponent<RawImage>().enabled = true;
                        }
                    }
                    productCounter ++;
                }
            }

            if(productCounter < ListaSpesa.listaSpesa.ElementAt(i).Value)
            {
                while(productCounter != ListaSpesa.listaSpesa.ElementAt(i).Value)
                {
                    slot = productInTable.GetChild(productCounter);
                    slot.GetComponent<RawImage>().enabled = true;
                    productCounter++;
                }
            }

        }

        foreach (var product in Carrello_controller.prodottiNelCarrello)
        {
            productShoppingListName = product.model.listName.Split('/')[0].ToLower();
            if (!ListaSpesa.listaSpesa.ContainsKey(productShoppingListName))
            {
                extraProductsList.GetChild(extraProductCounter).GetComponent<RawImage>().texture = product.GetComponent<icon>().myIcon;
                //extraProductsList.GetChild(extraProductCounter).GetComponent<RawImage>().enabled = true;
                extraProductCounter++;
            }
        }


    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        ResocontoActive = false;
        //SceneManager.LoadScene(0);
        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(0);
        yield return null;
    }

    public void Back()
    {
        ResocontoUI.SetActive(false);
        ResocontoActive = false;
        if (MenuPrincipale.levelDifficulty == 0)
        {
            ScenaFacile.SetActive(true);
        } else if (MenuPrincipale.levelDifficulty == 1)
        {
            ScenaMedia.SetActive(true);
        }
        else
        {
            ScenaDifficile.SetActive(true);
        }
    }


    public void Legenda()
    {
        if (legendaOpened)
        {
            StartCoroutine(CloseLegenda());
        }
        else
        {
            StartCoroutine(OpenLegenda());
        }

    }

    IEnumerator CloseLegenda()
    {
        LegendaAnim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        legendaOpened = false;
    }

    IEnumerator OpenLegenda()
    {
        LegendaAnim.SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        legendaOpened = true;
    }

    public void OggettiNonInLista()
    {
        if (oggettiNonInListaOpened)
        {
            StartCoroutine(CloseOggettiNonInLista());
        }
        else
        {
            StartCoroutine(OpenOggettiNonInLista());
        }

    }

    IEnumerator CloseOggettiNonInLista()
    {
        OggettiNonInListaAnim.SetTrigger("Close");
        yield return new WaitForSeconds(0.5f);
        oggettiNonInListaOpened = false;
    }

    IEnumerator OpenOggettiNonInLista()
    {
        OggettiNonInListaAnim.SetTrigger("Open");
        yield return new WaitForSeconds(0.5f);
        oggettiNonInListaOpened = true;
    }

}
