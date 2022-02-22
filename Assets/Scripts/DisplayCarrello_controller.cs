using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarrello_controller : MonoBehaviour
{
    //public Carrello_controller carrello;
    new public Camera camera;
    private Text prezzo;
    private Color defaultTextColor;
    private Color defaultBackgroundColor;
    private bool isDisplaying;
    // Start is called before the first frame update
    void Start()
    {
        isDisplaying = false;
        prezzo = GetComponentInChildren<Text>();
        defaultTextColor = prezzo.color;
        defaultBackgroundColor = camera.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDisplaying)
        {
            if(prezzo.fontSize == 196) prezzo.fontSize = 300;
            //prezzo.text = "€" + Mathf.Round(carrello.prezzo_totale*100)/100;
            prezzo.text = "€" + Mathf.Round(Carrello_controller.prezzo_totale_carrello * 100) / 100;
            if (Carrello_controller.prezzo_totale_carrello > ListaSpesa.budget)
            {
                prezzo.color = Color.black;
                camera.backgroundColor = new Color(201 / 255f, 22 / 255f, 10 / 255f);
            }
            else
            {
                camera.backgroundColor = defaultBackgroundColor;
                prezzo.color = defaultTextColor;
            }
        }
    }

    public void DisplayInventarioPieno()
    {
        StartCoroutine(InventarioPieno());
    }

    IEnumerator InventarioPieno()
    {
        isDisplaying = true;
        prezzo.fontSize = 196;
        prezzo.text = "CARRELLO PIENO!";
        prezzo.color = Color.black;
        camera.backgroundColor = new Color(201 / 255f, 22 / 255f, 10 / 255f);
        yield return new WaitForSecondsRealtime(3);
        isDisplaying = false;
    }
}
