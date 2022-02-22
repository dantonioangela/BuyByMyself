using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class icons_listaspesa : MonoBehaviour
{
     public Texture[] listIcons = new Texture[8];
    [System.NonSerialized] public bool[] hasIcon = new bool[8];
    // Start is called before the first frame update
    void Start()
    {
        listIcons.Initialize();
        for(int i = 0; i<8; i++)
        {
            hasIcon[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addIcon(string name, Texture icon)
    {
        int j = 0;
        foreach (var i in ListaSpesa.listaSpesa)
        {
            if(i.Key == name)
            {
                if (!hasIcon[j])
                {
                    listIcons[j] = icon;
                    hasIcon[j] = true;
                }
                break;
            }
            j++;
        }
    }
}
