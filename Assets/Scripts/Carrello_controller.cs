using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrello_controller : MonoBehaviour
{
    private GameObject[] busta = new GameObject[3];    
    public int mode = 0;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCarrello;
    private RaycastHit hitCarrello;
    private Collider carrelloCollider;
    private NavMeshObstacle navObstacle;
    public GameObject prefabBusta;
    private bool[] conBusta = new bool[3];
    private int numeroOggetti;
    private Transform parent;

    public float prezzo_totale;
    //DA CAMBIARE
    public float budget;


    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        carrelloCollider = GetComponent<Collider>();
        navObstacle = GetComponent<NavMeshObstacle>();
        prezzo_totale = 0.0f;
        numeroOggetti = 0;
        budget = 8f;
        conBusta[0] = false;
        conBusta[1] = false;
        conBusta[2] = false;
        transform.position = new Vector3(parent.position.x, 0f, parent.position.z) + 1.4f * parent.forward;
        transform.rotation = Quaternion.LookRotation(-parent.right, transform.up);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBuste();

        if (mode == 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mode = 1;
                carrelloCollider.enabled = true;
                navObstacle.enabled = true;
                transform.parent = null;
                //DA CAMBIARE
                prezzo_totale += 6.0f;
                numeroOggetti += 15;
            }
        }
        else if (mode == 1)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (Input.GetMouseButtonDown(0) && hit.collider.tag == "carrello")
                {
                    mode = 0;
                    carrelloCollider.enabled = false;
                    navObstacle.enabled = false;
                    //DA CAMBIARE
                    numeroOggetti -= 15;
                    transform.parent = parent;
                    transform.position = new Vector3(parent.position.x, 0f, parent.position.z) + 1.4f * parent.forward;
                    transform.rotation = Quaternion.LookRotation(-parent.right, transform.up);
                }
            }
        }

    }

    void UpdateBuste()
    {
        if (numeroOggetti > 0)
        {
            if (!conBusta[0])
            {
                AddBusta(0);
            }
            if (numeroOggetti > 5)
            {
                if (!conBusta[1])
                {
                    AddBusta(1);
                }
            }
            if (numeroOggetti > 10)
            {
                if (!conBusta[2])
                {
                    AddBusta(2);
                }
            }

        }
        if (conBusta[2] && numeroOggetti <= 10)
        {
            RemoveBusta(2);
        }
        if (conBusta[1] && numeroOggetti <= 5)
        {
            RemoveBusta(1);
        }
        if (conBusta[0] && numeroOggetti <= 0)
        {
            RemoveBusta(0);
        }
    }


    void AddBusta(int index)
    {
        busta[index] = Instantiate(prefabBusta, new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z), transform.rotation);
        busta[index].transform.localScale = new Vector3(0f, 0f, 0f);
        busta[index].transform.parent = transform;
        busta[index].transform.localPosition += new Vector3(0.5f, 0f, 0f) - new Vector3(index*0.6f, 0f, 0f);
        conBusta[index] = true;

        StartCoroutine(SpawnAnimation(index));
    }

    void RemoveBusta(int index)
    {
        Destroy(busta[index]);
        conBusta[index] = false;
    }

    IEnumerator SpawnAnimation( int index)
    {
        if (!conBusta[index])
        {
            yield break;
        }
        while(conBusta[index] && busta[index].transform.localScale.x < 0.5f)
        {
            busta[index].transform.localScale += new Vector3(0.8f * Time.deltaTime, 0.8f * Time.deltaTime, 0.8f * Time.deltaTime);
            yield return null;
        }
    }


}
