using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrello_controller : MonoBehaviour
{
    public GameObject player;
    public int mode = 0;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCarrello;
    private RaycastHit hitCarrello;
    private Collider carrelloCollider;
    private NavMeshObstacle navObstacle;
    //public bool dontMove = false;

    // Start is called before the first frame update
    void Start()
    {
        carrelloCollider = GetComponent<Collider>();
        navObstacle = GetComponent<NavMeshObstacle>();
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
                carrelloCollider.enabled = true;
                navObstacle.enabled = true;
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
                    carrelloCollider.enabled = false;
                    navObstacle.enabled = false;
                }
            }
        }

    }



}
