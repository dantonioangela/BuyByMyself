using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SchermataFinale : MonoBehaviour
{
    public float Massimo;
    public float TotaleGiocatore=50;
    public float PackagingGiocatore = 30;
    public float QualityGiocatore = 70;
    public float StagioneGiocatore = 10;
    public float ProvenienzaGiocatore = 90;
    public float PrezzoGiocatore = 20;
    public Image MaskTotale;
    public Image MaskPackaging;
    public Image MaskQuality;
    public Image MaskStagione;
    public Image MaskProvenienza;
    public Image MaskPrezzo;
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
        float fillAmountTotale = (float)TotaleGiocatore / (float)Massimo;
        MaskTotale.fillAmount = fillAmountTotale;

        float fillAmountPackaging = (float)PackagingGiocatore / (float)Massimo;
        MaskPackaging.fillAmount = fillAmountPackaging;

        float fillAmountQuality = (float)QualityGiocatore / (float)Massimo;
        MaskQuality.fillAmount = fillAmountQuality;

        float fillAmountStagione = (float)StagioneGiocatore / (float)Massimo;
        MaskStagione.fillAmount = fillAmountStagione;

        float fillAmountProvenienza = (float)ProvenienzaGiocatore / (float)Massimo;
        MaskProvenienza.fillAmount = fillAmountProvenienza;
        float fillAmountPrezzo = (float)PrezzoGiocatore / (float)Massimo;
        MaskPrezzo.fillAmount = fillAmountPrezzo;
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
}
