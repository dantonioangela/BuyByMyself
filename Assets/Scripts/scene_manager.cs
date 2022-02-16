using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_manager : MonoBehaviour
{
    private int seed;
    // Start is called before the first frame update

    /*private void Awake()
    {
        seed = (int)System.DateTime.Now.Ticks;
        Random.InitState(seed);
    }*/

    void Start()
    {

        transform.GetChild(0).gameObject.GetComponent<Loader>().StartMe();
        transform.GetChild(1).gameObject.GetComponent<ListaSpesa>().StartMe();
        for (int i =2; i< transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<Label_assigner>().StartMe();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
