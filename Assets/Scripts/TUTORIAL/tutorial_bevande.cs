using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_bevande : MonoBehaviour
{

    public bool tutorialStepStart = false;
    private Transform[] child = new Transform[37];
    int i = 0;
    // Start is called before the first frame update
    void Start()
    {

        for (i = 0; i < 37; i++)
        {
            child[i] = transform.GetChild(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStepStart)
        {
            for (i = 0; i < 37; i++)
            {
                child[i].GetComponent<MeshCollider>().enabled = true;
                child[i].GetComponent<tutorial_product>().enabled = true;
            }
        }
    }
}
