using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class season_panel_controller : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i< transform.childCount ; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);

        }


    }

    // Update is called once per frame
    /*void Update()
    {
        if (MenuPrincipale.levelDifficulty == 2 && ListaSpesa.setSeason)
        {
            transform.GetChild(ListaSpesa.season).gameObject.SetActive(true);
            ListaSpesa.setSeason = false;
        }
    }*/

    public void ActiveLavagna()
    {
        transform.GetChild(ListaSpesa.season).gameObject.SetActive(true);
    }
}
