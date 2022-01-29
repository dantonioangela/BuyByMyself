using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrello_controller : MonoBehaviour
{
    public GameObject player;
    private int mode = 0;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCarrello;
    private RaycastHit hitCarrello;
    private Collider carrelloCollider;
    private bool dontMove = false;

    // Start is called before the first frame update
    void Start()
    {
        carrelloCollider = GetComponent<Collider>();
        carrelloCollider.isTrigger = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (mode == 0)
        {

            transform.position = new Vector3(player.transform.position.x, 0f, player.transform.position.z) + 1.4f * player.transform.forward;
            transform.rotation = Quaternion.LookRotation(-player.transform.right, transform.up);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mode = 1;
            }
        }
        else if (mode == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 50.0f))
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.tag == "carrello")
                {
                    mode = 0;
                }
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        dontMove = true;
    }

    private void OnTriggerExit(Collider other)
    {
        dontMove = false;
    }


}
