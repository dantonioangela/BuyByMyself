using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cassiera_controller : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Animator animator;
    public bool isTalking = false;
    public Player_Controller player;
    private Transform wantTopay;
    private Transform cantPay;
    private GameObject speechCloud;
    private bool selected = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        wantTopay = transform.GetChild(2);
        cantPay = transform.GetChild(3);
        wantTopay.gameObject.SetActive(false);
        cantPay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!isTalking)
        {
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider.tag == "cassiera")
                {
                    GetComponentInChildren<isSelectable>().Select();
                    selected = true;
                    if (Input.GetMouseButtonDown(0))
                    {
                        GetComponentInChildren<isSelectable>().Deselect();
                        selected = false;
                        if (Carrello_controller.mode == 0)      //se ha il carrello
                        {
                            animator.SetTrigger("click");
                            isTalking = true;
                            speechCloud = wantTopay.gameObject;
                            speechCloud.SetActive(true);
                            speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                            Player_Controller.UI_active = true;
                        }
                        else
                        {
                            animator.SetTrigger("click");
                            isTalking = true;
                            speechCloud = cantPay.gameObject;
                            speechCloud.SetActive(true);
                            speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                            Player_Controller.UI_active = true;
                        }
                    }
                }
                else if (selected)
                {
                    GetComponentInChildren<isSelectable>().Deselect();
                    selected = false;
                }
                
            }
            else if (selected)
            {
                GetComponentInChildren<isSelectable>().Deselect();
                selected = false;
            }
        }
        if (isTalking)
        {
            speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation( player.gameObject.transform.right, transform.up  ));
        }
    }

    public void dontPay()
    {
        isTalking = false;
        wantTopay.gameObject.SetActive(false);
        animator.SetTrigger("click");
        Player_Controller.UI_active = false;
    }

    public void pay()
    {
        isTalking = false;
        wantTopay.gameObject.SetActive(false);
        Player_Controller.UI_active = false;
        animator.SetTrigger("payed");
        Result result = FinalResultCalculator.calculateFinalResult(Carrello_controller.prodottiNelCarrello, MenuPrincipale.levelDifficulty);
        Debug.Log("[Total points: " + result.totalPoints + " " +
                   "Quality points: " + (result.qualityPoints.HasValue ? result.qualityPoints.Value.ToString() : "NO") + " " +
                   "Eco points: " + (result.ecoPoints.HasValue ? result.ecoPoints.Value.ToString() : "NO") + " " +
                   "Price points: " + result.pricePoints + " " +
                   "Sustainability points: " + (result.sustainablePoints.HasValue ? result.sustainablePoints.Value.ToString() : "NO") + " " +
                   "Origin points: " + (result.originPoints.HasValue ? result.originPoints.Value.ToString() : "NO") + " " +
                   "Season points: " + (result.seasonPoints.HasValue ? result.seasonPoints.Value.ToString() : "NO"));
    }

    public void ok()
    {
        isTalking = false;
        speechCloud.SetActive(false);
        Player_Controller.UI_active = false;
        animator.SetTrigger("click");
    }
}
