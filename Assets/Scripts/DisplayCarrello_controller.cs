using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayCarrello_controller : MonoBehaviour
{
    public Carrello_controller carrello;
    new public Camera camera;
    private Text prezzo;
    private Color defaultTextColor;
    private Color defaultBackgroundColor;
    // Start is called before the first frame update
    void Start()
    {
        prezzo = GetComponentInChildren<Text>();
        defaultTextColor = prezzo.color;
        defaultBackgroundColor = camera.backgroundColor;
    }

    // Update is called once per frame
    void Update()
    {

        prezzo.text = "€" + Mathf.Round(carrello.prezzo_totale*100)/100;
        if(carrello.prezzo_totale > carrello.budget)
        {
            prezzo.color = Color.black;
            camera.backgroundColor = new Color(201/255f, 22/255f, 10/255f);
        }
        else
        {
            camera.backgroundColor = defaultBackgroundColor;
            prezzo.color = defaultTextColor;
        }
    }
}
