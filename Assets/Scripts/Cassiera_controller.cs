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
        if (!isTalking && Physics.Raycast(ray, out hit, 5.0f))
        {
            if (Input.GetMouseButtonDown(0) && hit.collider.tag == "cassiera")
            {
                if (player.carrello.mode == 0)      //se ha il carrello
                {
                    animator.SetTrigger("click");
                    isTalking = true;
                    speechCloud = wantTopay.gameObject;
                    speechCloud.SetActive(true);
                    speechCloud.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(player.gameObject.transform.right, transform.up));
                    player.UI_active = true;
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
}
