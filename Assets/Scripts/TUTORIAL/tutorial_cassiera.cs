using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_cassiera : MonoBehaviour
{
    private Ray ray;
    private RaycastHit hit;
    private Animator animator;
    public bool isTalking = false;
    public tutorial_player_controller player;
    private Transform wantTopay;
    private Transform cantPay;
    private GameObject speechCloud;
    private bool selected = false;

    public bool tutorialStepStart = false;
    public bool tutorialStepDone = false;
    public tutorial_canvas_controller speech;
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
        if (tutorialStepStart && !tutorialStepDone)
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
                            if (tutorial_carrello_controller.mode == 0)      //se ha il carrello
                            {

                                /*speech.ChangeSpeech(13);
                                animator.SetTrigger("click");
                                isTalking = true;
                                speechCloud = wantTopay.gameObject;
                                speechCloud.SetActive(true);
                                speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                                player.UI_active = true;*/
                                tutorialStepDone = true;
                                speech.ChangeSpeech(13);
                                pay();
                            }
                            else
                            {
                                animator.SetTrigger("click");
                                isTalking = true;
                                speechCloud = cantPay.gameObject;
                                speechCloud.SetActive(true);
                                speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                                player.UI_active = true;
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
        wantTopay.gameObject.SetActive(false);
        animator.SetTrigger("click");
        player.UI_active = false;
    }

    public void pay()
    {
        isTalking = false;
        wantTopay.gameObject.SetActive(false);
        player.UI_active = false;
        animator.SetTrigger("payed");
    }

    public void ok()
    {
        isTalking = false;
        speechCloud.SetActive(false);
        player.UI_active = false;
        animator.SetTrigger("click");
    }

    public void startTutorialStep()
    {
        tutorialStepStart = true;
    }
}
