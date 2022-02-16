using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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

    private Result result;

    public GameObject SchermataFacile;
    public GameObject SchermataMedia;
    public GameObject SchermataDifficile;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
    }

    void GetCurrentFill()
    {
        result = FinalResultCalculator.calculateFinalResult(Carrello_controller.prodottiNelCarrello, MenuPrincipale.levelDifficulty);

        float fillAmountTotale = 0;
        float fillAmountPrezzo = 0;
        float fillAmountPackaging = 0;
        float fillAmountQuality = 0;
        float fillAmountProvenienza = 0;
        float fillAmountStagione = 0;

        if (MenuPrincipale.levelDifficulty == 0)
        {
            fillAmountTotale = (float)result.totalPoints / (float)Massimo;
            fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
            MaskTotaleFacile.fillAmount = fillAmountTotale;
            MaskPrezzoFacile.fillAmount = fillAmountPrezzo;
            if (result.ecoPoints.HasValue)
            {
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            MaskPackagingFacile.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            MaskQualityFacile.fillAmount = fillAmountQuality;
        } else if (MenuPrincipale.levelDifficulty == 1)
        {
            fillAmountTotale = (float)result.totalPoints / (float)Massimo;
            fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
            MaskTotaleMedia.fillAmount = fillAmountTotale;
            MaskPrezzoMedia.fillAmount = fillAmountPrezzo;
            if (result.ecoPoints.HasValue)
            {
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            MaskPackagingMedia.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            MaskQualityMedia.fillAmount = fillAmountQuality;
            if (result.originPoints.HasValue)
            {
                fillAmountProvenienza = (float)result.originPoints.Value / (float)Massimo;
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
                fillAmountPackaging = (float)result.ecoPoints.Value / (float)Massimo;
            }
            MaskPackagingDifficile.fillAmount = fillAmountPackaging;
            if (result.qualityPoints.HasValue)
            {
                fillAmountQuality = (float)result.qualityPoints.Value / (float)Massimo;
            }
            MaskQualityDifficile.fillAmount = fillAmountQuality;
            if (result.originPoints.HasValue)
            {
                fillAmountProvenienza = (float)result.originPoints.Value / (float)Massimo;
            }
            MaskProvenienzaDifficile.fillAmount = fillAmountProvenienza;
            if (result.seasonPoints.HasValue)
            {
                fillAmountStagione = (float)result.seasonPoints.Value / (float)Massimo;
            }
            MaskStagioneDifficile.fillAmount = fillAmountStagione;
        }       
        
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        //SceneManager.LoadScene(0);
        StartCoroutine(Reload());
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
    }

    private IEnumerator Reload()
    {
        Resources.UnloadUnusedAssets();
        SceneManager.LoadScene(0);
        yield return null;
    }
}
