using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Carrello_controller : MonoBehaviour
{
    private GameObject[] busta = new GameObject[3];
    public static List<Product> prodottiNelCarrello = new List<Product>();
    public static int mode = 0;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCarrello;
    private RaycastHit hitCarrello;
    private Collider carrelloCollider;
    private NavMeshObstacle navObstacle;
    [SerializeField] private GameObject prefabBusta;
    private bool[] conBusta = new bool[3];
    private Transform parent;
    public static bool selected = false;
    public static float prezzo_totale_carrello;
    [SerializeField] private UI_controller inventario;
    //DA CAMBIARE
    private float budget;
    private GameObject mySelf;


    // Start is called before the first frame update
    void Start()
    {
        mode = 0;
        selected = false;
        budget = ListaSpesa.budget;
        parent = transform.parent;
        carrelloCollider = GetComponent<Collider>();
        navObstacle = GetComponent<NavMeshObstacle>();
        prezzo_totale_carrello = 0.0f;
        budget = 8f;
        conBusta[0] = false;
        conBusta[1] = false;
        conBusta[2] = false;
        transform.position = new Vector3(parent.position.x, 0f, parent.position.z) + 1.4f * parent.forward;
        transform.rotation = Quaternion.LookRotation(-parent.right, transform.up);
        mySelf = gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBuste();

        if (mode == 0)                                  //ho il carrello agganciato
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                mode = 1;
                carrelloCollider.enabled = true;
                navObstacle.enabled = true;
                transform.parent = null;
                //DA CAMBIARE
            }
        }
        else if (mode == 1)                             //non ho il carrello
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider.tag == "carrello")
                {
                    if (!selected)
                    {
                        GetComponent<isSelectable>().Select();
                        selected = true;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponent<isSelectable>().Deselect();
                        selected = false;
                        mode = 0;
                        carrelloCollider.enabled = false;
                        navObstacle.enabled = false;
                        transform.parent = parent;
                        transform.position = new Vector3(parent.position.x, 0f, parent.position.z) + 1.4f * parent.forward;
                        transform.rotation = Quaternion.LookRotation(-parent.right, transform.up);
                    }
                }
                else if (selected)
                {
                    GetComponent<isSelectable>().Deselect();
                    selected = false;
                }
            }
            else if (selected)
            {
                GetComponent<isSelectable>().Deselect();
                selected = false;
            }
        }

    }

    void UpdateBuste()
    {
        if ( prodottiNelCarrello.Count > 0)
        {
            if (!conBusta[0])
            {
                AddBusta(0);
            }
            if (prodottiNelCarrello.Count > 5)
            {
                if (!conBusta[1])
                {
                    AddBusta(1);
                }
            }
            if (prodottiNelCarrello.Count > 10)
            {
                if (!conBusta[2])
                {
                    AddBusta(2);
                }
            }

        }
        if (conBusta[2] && prodottiNelCarrello.Count <= 10)
        {
            RemoveBusta(2);
        }
        if (conBusta[1] && prodottiNelCarrello.Count <= 5)
        {
            RemoveBusta(1);
        }
        if (conBusta[0] && prodottiNelCarrello.Count <= 0)
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

    public void AddProductToChart (Product product)
    {
        prezzo_totale_carrello += product.GetComponent<Product>().model.price;
        prodottiNelCarrello.Add(product);
        
        inventario.AddProductToInventario(product);
        
    }

    public void RemoveProductFromChart (Product product)
    {
        if (prodottiNelCarrello.Contains(product))
        {
            prodottiNelCarrello.Remove(product);
            prezzo_totale_carrello -= product.GetComponent<Product>().model.price;
            product.transform.position = product.GetComponent<Product>().position;
            product.transform.rotation = product.GetComponent<Product>().rotation;
            product.GetComponent<Collider>().enabled = true;
        }
    }

}
