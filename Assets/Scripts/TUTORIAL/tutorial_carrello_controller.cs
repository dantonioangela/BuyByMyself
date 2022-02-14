using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class tutorial_carrello_controller : MonoBehaviour
{


    public static bool tutorialStepBananeStart = false;
    private bool tutorialStepBananeDone = false;
    private bool tutorialStepBibitaDone = false;
    private bool tutorialStepSalmoneDone = false;
    public tutorial_canvas_controller speech;
    public tutorial_vetro_controller vetro;
    public tutorial_bevande bevande;
    public GameObject banana2;


    private GameObject[] busta = new GameObject[3];
    public static int mode = 0;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCarrello;
    private RaycastHit hitCarrello;
    private Collider carrelloCollider;
    private NavMeshObstacle navObstacle;
    [SerializeField] private GameObject prefabBusta;
    private bool[] conBusta = new bool[3];
    private int numeroOggetti;
    private Transform parent;
    public static bool selected = false;
    public static float prezzo_totale_carrello;
    [SerializeField] private tutorial_UI inventario;
    //DA CAMBIARE
    [System.NonSerialized] public static float budget;
    private GameObject mySelf;


    // Start is called before the first frame update
    void Start()
    {
        budget = 7f;
        parent = transform.parent;
        carrelloCollider = GetComponent<Collider>();
        navObstacle = GetComponent<NavMeshObstacle>();
        prezzo_totale_carrello = 0.0f;
        numeroOggetti = 0;
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
        else if (mode == 1 && tutorialStepBananeStart)                             //non ho il carrello
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
                        //DA CAMBIARE
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
        busta[index].transform.localPosition += new Vector3(0.5f, 0f, 0f) - new Vector3(index * 0.6f, 0f, 0f);
        conBusta[index] = true;

        StartCoroutine(SpawnAnimation(index));
    }

    void RemoveBusta(int index)
    {
        Destroy(busta[index]);
        conBusta[index] = false;
    }

    IEnumerator SpawnAnimation(int index)
    {
        if (!conBusta[index])
        {
            yield break;
        }
        while (conBusta[index] && busta[index].transform.localScale.x < 0.5f)
        {
            busta[index].transform.localScale += new Vector3(0.8f * Time.deltaTime, 0.8f * Time.deltaTime, 0.8f * Time.deltaTime);
            yield return null;
        }
    }

    public void AddProductToChart(GameObject product)
    {
        if (numeroOggetti < 15)
        {
            if (numeroOggetti == 0)
            {
                banana2.GetComponent<MeshCollider>().enabled = true;
                banana2.GetComponent<tutorial_product>().enabled = true;
            }
            if (numeroOggetti == 1 && !tutorialStepBananeDone)
            {
                speech.ChangeSpeech(5);
                bevande.tutorialStepStart = true;
                tutorialStepBananeDone = true;
            }
            if (numeroOggetti == 2 && !tutorialStepBibitaDone)
            {
                speech.ChangeSpeech(6);
                tutorialStepBibitaDone = true;
                bevande.tutorialStepStart = false;
                vetro.tutorialStepVetroStart = true;
                vetro.tutorialStepSalmoneStart = true;
            }
            if (numeroOggetti == 3 && !tutorialStepSalmoneDone)
            {
                speech.ChangeSpeech(9);
                tutorialStepSalmoneDone = true;
                parent.GetComponent<tutorial_player_controller>().tutorialStepInventarioStart = true;
                vetro.tutorialStepSalmoneDone = true;
            }
            numeroOggetti++;
            prezzo_totale_carrello += product.GetComponent<tutorial_product>().price;

            inventario.AddProductToInventario(product);
        }
        else
        {
            Debug.Log("inventario pieno");
        }

    }

    public void RemoveProductFromChart(GameObject product)
    {
        numeroOggetti--;
        prezzo_totale_carrello -= product.GetComponent<tutorial_product>().price;
        product.transform.position += new Vector3(0f, 10f, 0f);
        //product.transform.parent = null;
    }
}
