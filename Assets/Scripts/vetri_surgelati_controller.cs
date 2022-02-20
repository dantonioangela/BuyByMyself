using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vetri_surgelati_controller : MonoBehaviour
{
    private GameObject[] child = new GameObject[3];
    private Vector3[] pos_init = new Vector3[3];
    private float z_spostamento = 0.002f;
    private float x_spostamento;
    private bool[] isSelected = new bool[3];
    private int[] aperto = new int[3];
    private Ray ray;
    private RaycastHit hit;
    private float speed_z = 1f;
    private float speed_x = 3f;
    public AudioManager audioMan;

    // Start is called before the first frame update
    void Start()
    {
        isSelected[0] = false;
        isSelected[1] = false;
        isSelected[2] = false;
        aperto[0] = 0;
        aperto[1] = 0;
        aperto[2] = 0;
        child[0] = transform.GetChild(0).gameObject;
        child[1] = transform.GetChild(1).gameObject;
        child[2] = transform.GetChild(2).gameObject;
        pos_init[0] = child[0].transform.localPosition;
        pos_init[1] = child[1].transform.localPosition;
        pos_init[2] = child[2].transform.localPosition;
        x_spostamento = pos_init[0].x - pos_init[1].x;

    }

    // Update is called once per frame
    void Update()
    {
        if (!Player_Controller.UI_active)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider == child[0].GetComponent<Collider>())
                {
                    if (!isSelected[0])
                    {
                        if (isSelected[1])
                        {
                            isSelected[1] = false;
                            child[1].GetComponent<isSelectable>().Deselect();
                        }
                        else if (isSelected[2])
                        {
                            isSelected[2] = false;
                            child[2].GetComponent<isSelectable>().Deselect();
                        }
                        isSelected[0] = true;
                        child[0].GetComponent<isSelectable>().Select();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        aperto[0]++;
                        if (aperto[0] == 1)
                        {
                            if (aperto[1] == 1)
                            {
                                StartCoroutine(GoBack(1));
                                aperto[0] = 3;
                            }
                            else if (aperto[2] == 1)
                            {
                                StartCoroutine(GoBack(2));
                                aperto[0] = 3;
                            }
                            else StartCoroutine(ToRight(0));
                        }
                        if (aperto[0] == 2)
                        {
                            if (aperto[2] == 1) StartCoroutine(GoBack(2));
                            StartCoroutine(GoBack(0));
                        }
                    }

                }
                else if (hit.collider == child[1].GetComponent<Collider>())
                {
                    if (!isSelected[1])
                    {
                        if (isSelected[0])
                        {
                            isSelected[0] = false;
                            child[0].GetComponent<isSelectable>().Deselect();
                        }
                        else if (isSelected[2])
                        {
                            isSelected[2] = false;
                            child[2].GetComponent<isSelectable>().Deselect();
                        }
                        isSelected[1] = true;
                        child[1].GetComponent<isSelectable>().Select();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        aperto[1]++;
                        if (aperto[1] == 1)
                        {
                            if (aperto[0] == 1)
                            {
                                StartCoroutine(GoBack(0));
                                aperto[1] = 3;
                            }
                            else
                            {
                                if (aperto[2] == 1) StartCoroutine(GoBack(2));
                                StartCoroutine(ToLeft(1));
                            }
                        }
                        if (aperto[1] == 2)
                        {
                            StartCoroutine(GoBack(1));
                        }
                    }
                }
                else if (hit.collider == child[2].GetComponent<Collider>())
                {
                    if (!isSelected[2])
                    {
                        if (isSelected[1])
                        {
                            isSelected[1] = false;
                            child[1].GetComponent<isSelectable>().Deselect();
                        }
                        else if (isSelected[0])
                        {
                            isSelected[0] = false;
                            child[0].GetComponent<isSelectable>().Deselect();
                        }
                        isSelected[2] = true;
                        child[2].GetComponent<isSelectable>().Select();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        aperto[2]++;
                        if (aperto[2] == 1)
                        {
                            if (aperto[1] == 1) StartCoroutine(GoBack(1));
                            StartCoroutine(ToRight(2));
                        }
                        if (aperto[2] == 2)
                        {
                            StartCoroutine(GoBack(2));
                        }
                    }
                }
                else if (isSelected[0] || isSelected[1] || isSelected[2])
                {
                    child[0].GetComponent<isSelectable>().Deselect();
                    child[1].GetComponent<isSelectable>().Deselect();
                    child[2].GetComponent<isSelectable>().Deselect();
                    isSelected[0] = false;
                    isSelected[1] = false;
                    isSelected[2] = false;
                }
            }
            else if (isSelected[0] || isSelected[1] || isSelected[2])
            {
                child[0].GetComponent<isSelectable>().Deselect();
                child[1].GetComponent<isSelectable>().Deselect();
                child[2].GetComponent<isSelectable>().Deselect();
                isSelected[0] = false;
                isSelected[1] = false;
                isSelected[2] = false;
            }
        }
    }

    private IEnumerator ToRight (int i)
    {
        audioMan.PlayInstance("frigo");
        while (child[i].transform.localPosition.y > pos_init[i].y - z_spostamento)
        {
            child[i].transform.position += child[i].transform.up * Time.deltaTime * speed_z;
            yield return new WaitForEndOfFrame();
        }
        while (child[i].transform.localPosition.x > pos_init[i].x - x_spostamento)
        {
            child[i].transform.position += -child[i].transform.right * Time.deltaTime * speed_x;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator ToLeft(int i)
    {
        audioMan.PlayInstance("frigo");
        while (child[i].transform.localPosition.y > pos_init[i].y - z_spostamento)
        {
            child[i].transform.position += child[i].transform.up * Time.deltaTime * speed_z;
            yield return new WaitForEndOfFrame();
        }
        while (child[i].transform.localPosition.x < pos_init[i].x + x_spostamento)
        {
            child[i].transform.position -= -child[i].transform.right * Time.deltaTime * speed_x;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator GoBack(int i)
    {
        audioMan.PlayInstance("frigo");
        if(i != 1)
        {
            while (child[i].transform.localPosition.x < pos_init[i].x)
            {
                child[i].transform.position -= -child[i].transform.right * Time.deltaTime * speed_x;
                yield return new WaitForEndOfFrame();
            }
        }
        else
        {
            while (child[i].transform.localPosition.x > pos_init[i].x)
            {
                child[i].transform.position += -child[i].transform.right * Time.deltaTime * speed_x;
                yield return new WaitForEndOfFrame();
            }
        }
        while (child[i].transform.localPosition.y < pos_init[i].y)
        {
            child[i].transform.position -= child[i].transform.up * Time.deltaTime * speed_z;
            yield return new WaitForEndOfFrame();
        }
        aperto[i] = 0;
        if(aperto[0] == 3)
        {
            StartCoroutine(ToRight(0));
            aperto[0] = 1;
        }
        if(aperto[1] == 3)
        {
            StartCoroutine(ToLeft(1));
            aperto[1] = 1;
        }
        yield return null;
    }
}
