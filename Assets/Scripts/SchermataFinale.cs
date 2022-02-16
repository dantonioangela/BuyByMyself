using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SchermataFinale : MonoBehaviour
{
    public float Massimo=100;
    public Image MaskTotale;
    public Image MaskPackaging;
    public Image MaskQuality;
    public Image MaskStagione;
    public Image MaskProvenienza;
    public Image MaskPrezzo;

    private Result result;

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

        float fillAmountTotale = (float)result.totalPoints/ (float)Massimo;
        MaskTotale.fillAmount = fillAmountTotale;

        float fillAmountPackaging = (float)result.ecoPoints / (float)Massimo;
        MaskPackaging.fillAmount = fillAmountPackaging;

        float fillAmountQuality = (float)result.qualityPoints/ (float)Massimo;
        MaskQuality.fillAmount = fillAmountQuality;

        float fillAmountStagione = (float)result.seasonPoints/ (float)Massimo;
        MaskStagione.fillAmount = fillAmountStagione;

        float fillAmountProvenienza = (float)result.originPoints / (float)Massimo;
        MaskProvenienza.fillAmount = fillAmountProvenienza;

        float fillAmountPrezzo = (float)result.pricePoints / (float)Massimo;
        MaskPrezzo.fillAmount = fillAmountPrezzo;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
