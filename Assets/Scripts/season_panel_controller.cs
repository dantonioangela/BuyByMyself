using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class season_panel_controller : MonoBehaviour
{
    private bool done = false;
    // Start is called before the first frame update
    void Start()
    {
        done = false;
        for(int i = 0; i< transform.childCount ; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);

        }


    }

    // Update is called once per frame
    void Update()
    {
        if (MenuPrincipale.levelDifficulty == 2 && ListaSpesa.setSeason && !done)
        {
            transform.GetChild(ListaSpesa.season).gameObject.SetActive(true);
            //ListaSpesa.setSeason = false;
            done = false;
        }
    }

    /*public void ActiveLavagna(int s)
    {
        if (MenuPrincipale.levelDifficulty == 2)
        {
            transform.GetChild(s).gameObject.SetActive(true);
        }
    }*/
}
