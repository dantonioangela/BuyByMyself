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
    public bool canPay = true;
    bool gameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        isTalking = false;
        selected = false;
        canPay = true;
        gameOver = false;
        animator = GetComponent<Animator>();
        wantTopay = transform.GetChild(2);
        cantPay = transform.GetChild(3);
        wantTopay.gameObject.SetActive(false);
        cantPay.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameOver && !Player_Controller.UI_active)
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
                                if (Carrello_controller.prezzo_totale_carrello > ListaSpesa.budget)
                                {
                                    canPay = false;
                                    speechCloud = cantPay.gameObject;
                                    speechCloud.SetActive(true);
                                    speechCloud.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                                    speechCloud.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
                                }
                                else
                                {
                                    canPay = true;
                                    speechCloud = wantTopay.gameObject;
                                    speechCloud.SetActive(true);
                                }
                                speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                                Player_Controller.UI_active = true;
                            }
                            else
                            {
                                canPay = false;
                                animator.SetTrigger("click");
                                isTalking = true;
                                speechCloud = cantPay.gameObject;
                                speechCloud.SetActive(true);
                                speechCloud.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                                speechCloud.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
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
                speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
            }
        }
    }

    public void dontPay()
    {
        isTalking = false;
        //wantTopay.gameObject.SetActive(false);
        speechCloud.SetActive(false);
        animator.SetTrigger("click");
        Player_Controller.UI_active = false;
    }

    public void pay()
    {
        isTalking = false;
        //wantTopay.gameObject.SetActive(false);
        speechCloud.SetActive(false);
        Player_Controller.UI_active = false;
        animator.SetTrigger("payed");
        Result result = FinalResultCalculator.calculateFinalResult(Carrello_controller.prodottiNelCarrello, MenuPrincipale.levelDifficulty);
    }

    public void ok()
    {
        isTalking = false;
        speechCloud.SetActive(false);
        Player_Controller.UI_active = false;
        animator.SetTrigger("click");
    }

    public void GameOver()
    {
        gameOver = true; 
    }
}
