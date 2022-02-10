using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_inventario : MonoBehaviour
{
    private int i;
    private int flag = -1;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddProduct(GameObject product)
    {
        flag = -1;
        for (i = 0; i < 15; i++)
        {
            if (transform.GetChild(i * 2).GetComponent<tutorial_slot_inventario>().productInThisSlot == product)
            {
                flag = i;
                break;
            }
        }
        if (flag == -1)
        {
            for (i = 0; i < 15; i++)
            {
                if (transform.GetChild(i * 2).GetComponent<tutorial_slot_inventario>().slotEmpty)
                {
                    transform.GetChild(i * 2).GetComponent<tutorial_slot_inventario>().AddProductInSlot(product);
                    break;
                }
            }
        }
    }


}
