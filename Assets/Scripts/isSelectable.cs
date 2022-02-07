using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isSelectable : MonoBehaviour
{
    //private Texture albedo;
    //private float intensity = 0.5f;
    private bool isSelected = false;
    private int i;
    // Start is called before the first frame update
    void Start()
    {
        for (i = 0; i < GetComponent<Renderer>().materials.Length; i++)
        {
            GetComponent<Renderer>().materials[i].SetFloat("Highlight", 0f);
        }

        /*
        gameObject.AddComponent<Outline>();
        gameObject.GetComponent<Outline>().OutlineMode = Outline.Mode.OutlineVisible;
        gameObject.GetComponent<Outline>().OutlineColor = new Color(1f, 1f, 1f, 1f);
        gameObject.GetComponent<Outline>().OutlineWidth = 10f;
        gameObject.GetComponent<Outline>().enabled = false;*/
    }

    // Update is called once per frame
    public void Select()
    {
        if (!isSelected)
        {
            for (i = 0; i < GetComponent<Renderer>().materials.Length; i++)
            {
                GetComponent<Renderer>().materials[i].SetFloat("Highlight", 1f);
            }
            //gameObject.GetComponent<Outline>().enabled = true;
            isSelected = true;
        }
    }

    public void Deselect()
    {
        if (isSelected)
        {
            for (i = 0; i < GetComponent<Renderer>().materials.Length; i++)
            {
                GetComponent<Renderer>().materials[i].SetFloat("Highlight", 0f);
            }
            //gameObject.GetComponent<Outline>().enabled = false;

            isSelected = false;
        }
    }
}
