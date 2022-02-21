using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        for (int i=0; i<Carrello_controller.prodottiNelCarrello.Count; i++)
        {
            RawImage im = ResocontoUI.transform.GetChild(i).GetComponent<RawImage>();
            im.texture = Carrello_controller.prodottiNelCarrello[i].GetComponent<icon>().myIcon;
            ResocontoUI.transform.GetChild(i).gameObject.SetActive(true);
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
