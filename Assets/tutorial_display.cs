
using UnityEngine;
using UnityEngine.UI;

public class tutorial_display : MonoBehaviour
{
    // Start is called before the first frame update
    //public Carrello_controller carrello;
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

        //prezzo.text = "€" + Mathf.Round(carrello.prezzo_totale*100)/100;
        prezzo.text = "€" + Mathf.Round(tutorial_carrello_controller.prezzo_totale_carrello * 100) / 100;
        if (tutorial_carrello_controller.prezzo_totale_carrello > tutorial_carrello_controller.budget)
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
