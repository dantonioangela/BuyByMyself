using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scene_manager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.GetChild(0).gameObject.GetComponent<Loader>().StartMe();
        transform.GetChild(1).gameObject.GetComponent<ListaSpesa>().StartMe();
        transform.GetChild(2).gameObject.GetComponent<Label_assigner>().StartMe();
        //transform.GetChild(3).gameObject.GetComponent<Label_assigner>().StartMe();
        //transform.GetChild(4).gameObject.GetComponent<Label_assigner>().StartMe();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
