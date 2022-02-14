using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventario_manager : MonoBehaviour
{
    private int i;
    private int flag = -1;
    public inventario_manager otherPage;
    public Button myButton;
    public Button nextButton;
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
        for(i=0; i < transform.childCount; i++)
        {
            if( transform.GetChild(i).GetComponent<slot_inventario_controller>().productInThisSlot == product )
            {
                flag = i;
                break;
            }
        }
        if(flag == -1)
        {
            for (i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<slot_inventario_controller>().slotEmpty )
                {
                    transform.GetChild(i).GetComponent<slot_inventario_controller>().AddProductInSlot(product);
                    break;
                }
            }
        }
    }

    public void NextPage()
    {
        Debug.Log("aa");
        myButton.gameObject.SetActive(false);
        nextButton.gameObject.SetActive(true);
        otherPage.gameObject.SetActive(true);
        gameObject.SetActive(false) ;
    }

    public void ReplaceProduct(int child, GameObject productReplace)
    {
        transform.GetChild(child).GetComponent<slot_inventario_controller>().ReplaceProduct();

    }
}
