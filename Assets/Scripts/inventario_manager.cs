using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inventario_manager : MonoBehaviour
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
        for(i=0; i < 15; i++)
        {
            if( transform.GetChild(i).GetComponent<slot_inventario_controller>().productInThisSlot == product )
            {
                flag = i;
                break;
            }
        }
        if(flag == -1)
        {
            for (i = 0; i < 15; i++)
            {
                if (transform.GetChild(i).GetComponent<slot_inventario_controller>().slotEmpty )
                {
                    transform.GetChild(i).GetComponent<slot_inventario_controller>().AddProductInSlot(product);
                    break;
                }
            }
        }
    }


}
