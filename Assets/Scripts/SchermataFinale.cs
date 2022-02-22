using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SchermataFinale : MonoBehaviour
{
    public float Massimo=100;

    public Image MaskTotaleFacile;
    public Image MaskTotaleMedia;
    public Image MaskTotaleDifficile;

    public Image MaskPackagingFacile;
    public Image MaskPackagingMedia;
    public Image MaskPackagingDifficile;

    public Image MaskQualityFacile;
    public Image MaskQualityMedia;
    public Image MaskQualityDifficile;

    public Image MaskStagioneDifficile;

    public Image MaskProvenienzaMedia;
    public Image MaskProvenienzaDifficile;

    public Image MaskPrezzoFacile;
    public Image MaskPrezzoMedia;
    public Image MaskPrezzoDifficile;

    public Image MaskScadenzeFacile;
    public Image MaskScadenzeMedia;
    public Image MaskScadenzeDifficile;

    private Result result;

    public GameObject SchermataFacile;
    public GameObject SchermataMedia;
    public GameObject SchermataDifficile;

    private float fillAmountTotale = 0f;
    private float fillAmountPrezzo = 0f;
    private float fillAmountPackaging = 0f;
    private float fillAmountQuality = 0f;
    private float fillAmountProvenienza = 0f;
    private float fillAmountStagione = 0f;
    private float fillAmountScadenze = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GetCurrentFill();
    }

    void GetCurrentFill()
    {
        result = FinalResultCalculator.calculateFinalResult(Carrello_controller.prodottiNelCarrello, MenuPrincipale.levelDifficulty);

        fillAmountTotale = 0f;
        fillAmountPrezzo = 0f;
        fillAmountPackaging = 0f;
        fillAmountQuality = 0f;
        fillAmountProvenienza = 0f;
        fillAmountStagione = 0f;
        fillAmountScadenze = 0f;

        if (MenuPrincipale.levelDifficulty == 0)
        {
            fillAmountTotale = (float)result.totalPoints / (float)Massimo;
            fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
            MaskTotaleFacile.fillAmount = fillAmountTotale;
            MaskPrezzoFacile.fillAmount = fillAmountPrezzo;
            if (result.ecoPoints.HasValue)
            {
                transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            }
            MaskPackagingFacile.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            }
            MaskQualityFacile.fillAmount = fillAmountQuality;
        } 
        
        else if (MenuPrincipale.levelDifficulty == 1)
        {
            fillAmountTotale = (float)result.totalPoints / (float)Massimo;
            fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
            MaskTotaleMedia.fillAmount = fillAmountTotale;
            MaskPrezzoMedia.fillAmount = fillAmountPrezzo;
            if (result.ecoPoints.HasValue)
            {
                transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            }
            MaskPackagingMedia.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            }
            MaskQualityMedia.fillAmount = fillAmountQuality;
            if (result.originPoints.HasValue)
            {
                transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
                fillAmountProvenienza = (float)result.originPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            }
            MaskProvenienzaMedia.fillAmount = fillAmountProvenienza;
        }

        else
        {
            fillAmountTotale = (float)result.totalPoints / (float)Massimo;
            fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
            MaskTotaleDifficile.fillAmount = fillAmountTotale;
            MaskPrezzoDifficile.fillAmount = fillAmountPrezzo;
            if (result.ecoPoints.HasValue)
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            MaskPackagingDifficile.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
            }
            MaskQualityDifficile.fillAmount = fillAmountQuality;
            if (result.originPoints.HasValue)
            {
                transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
                fillAmountProvenienza = (float)result.originPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(0).GetChild(2).gameObject.SetActive(false);
            }
            MaskProvenienzaDifficile.fillAmount = fillAmountProvenienza;
            if (result.seasonPoints.HasValue)
            {
                transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
                fillAmountStagione = (float)result.seasonPoints.Value / (float)Massimo;
            }
            else
            {
                transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
            }
            MaskStagioneDifficile.fillAmount = fillAmountStagione;
        }       
        
    }

    public void ScelataSchermata()
    {
        if (MenuPrincipale.levelDifficulty == 0)
        {
            SchermataFacile.SetActive(true);
            
        } else if(MenuPrincipale.levelDifficulty == 1)
        {
            SchermataMedia.SetActive(true);
        }
        else
        {
            SchermataDifficile.SetActive(true);
        }
        GetCurrentFill();
    }

 

    public void Resoconto()
    {
        if (MenuPrincipale.levelDifficulty == 0)
        {
            SchermataFacile.SetActive(false);
        } else if (MenuPrincipale.levelDifficulty == 1)
        {
            SchermataMedia.SetActive(false);
        } else
        {
            SchermataDifficile.SetActive(false);
        }

        Resoconti.ResocontoActive = true;
    }
}
