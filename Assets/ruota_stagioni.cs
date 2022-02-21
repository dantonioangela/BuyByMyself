using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ruota_stagioni : MonoBehaviour
{
    // Start is called before the first frame update
    private Ray ray;
    private RaycastHit hit;
    private bool isSelected = false;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {

        speed = 2f;
        isSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Player_Controller.UI_active)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider == GetComponent<Collider>())
                {
                    if (!isSelected)
                    {
                        isSelected = true;
                        GetComponentInChildren<isSelectable>().Select();

                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        StartCoroutine(RotateToNextSeason());
                    }
                }
                else if (isSelected)
                {
                    GetComponentInChildren<isSelectable>().Deselect();
                    isSelected = false;
                }

            }
            else if (isSelected)
            {
                GetComponentInChildren<isSelectable>().Deselect();
                isSelected = false;
            }
        }
    }

    IEnumerator RotateToNextSeason()
    {
        float slider = 0;
        while (slider < 1)
        {
            slider += 1 * speed * Time.deltaTime;
            transform.RotateAround(transform.position, transform.up, -90f*speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }
}
