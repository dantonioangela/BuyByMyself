using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_inventario : MonoBehaviour
{
    private int i;
    private int flag = -1;
    public bool tutorialStepViaBananeStart = false;
    private bool tutorialStepDone = false;
    public tutorial_canvas_controller speech;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(tutorialStepViaBananeStart && transform.GetChild(1).GetComponent<tutorial_slot_inventario>().slotEmpty && !tutorialStepDone)
        {
            tutorialStepDone = true;
            speech.ChangeSpeech(11);
        }
    }

    public void AddProduct(GameObject product)
    {
        flag = -1;
        for (i = 0; i < 15; i++)
        {
            if (transform.GetChild(i).GetComponent<tutorial_slot_inventario>().productInThisSlot == product)
            {
                flag = i;
                break;
            }
        }
        if (flag == -1)
        {
            for (i = 0; i < 15; i++)
            {
                if (transform.GetChild(i).GetComponent<tutorial_slot_inventario>().slotEmpty)
                {
                    transform.GetChild(i).GetComponent<tutorial_slot_inventario>().AddProductInSlot(product);
                    break;
                }
            }
        }
    }


}
