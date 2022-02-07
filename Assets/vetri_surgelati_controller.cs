using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class vetri_surgelati_controller : MonoBehaviour
{
    private GameObject[] child = new GameObject[3];
    private Vector3[] pos_init = new Vector3[3];
    public float z_spostamento;
    public float x_spostamento;
    private bool[] isSelected = new bool[3];
    private Ray ray;
    private RaycastHit hit;
    public float speed_z = 0.5f;
    public float speed_x = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        isSelected[0] = false;
        isSelected[1] = false;
        isSelected[2] = false;
        child[0] = transform.GetChild(0).gameObject;
        child[1] = transform.GetChild(1).gameObject;
        child[2] = transform.GetChild(2).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 5.0f))
        {
            if(hit.collider == child[0].GetComponent<Collider>() )
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
                    GoBack(1);
                    GoBack(2);
                    ToRight(0);
                }

            }
            else if(hit.collider == child[1].GetComponent<Collider>())
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
                    GoBack(0);
                    GoBack(2);
                    ToLeft(1);
                }
            }
            else if(hit.collider == child[2].GetComponent<Collider>())
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
                    GoBack(0);
                    GoBack(1);
                    ToRight(2);
                }
            }
            else if(isSelected[0] || isSelected[1] || isSelected[2])
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

    IEnumerator ToRight (int i)
    {
        while (child[i].transform.position.z < pos_init[i].z + z_spostamento)
        {
            child[i].transform.Translate(0f, 0f, z_spostamento * speed_z * Time.deltaTime);
            yield return null;
        }
        while (child[i].transform.position.x < pos_init[i].x + x_spostamento)
        {
            child[i].transform.Translate(x_spostamento * speed_x * Time.deltaTime, 0f, 0f);
            yield return null;
        }
        yield return null;
    }

    IEnumerator ToLeft(int i)
    {
        while (child[i].transform.position.z < pos_init[i].z + z_spostamento)
        {
            child[i].transform.Translate(0f, 0f, z_spostamento * speed_z * Time.deltaTime);
            yield return null;
        }
        while (child[i].transform.position.x > pos_init[i].x - x_spostamento)
        {
            child[i].transform.Translate(-x_spostamento * speed_x * Time.deltaTime, 0f, 0f );
            yield return null;
        }
        yield return null;
    }

    IEnumerator GoBack(int i)
    {
        while (child[i].transform.position.z > pos_init[i].z)
        {
            child[i].transform.Translate(0f, 0f, -z_spostamento * speed_z * Time.deltaTime);
            yield return null;
        }
        while (child[i].transform.position.x != pos_init[i].x)
        {
            if (i == 1)
            {
                child[i].transform.Translate(x_spostamento * speed_x * Time.deltaTime, 0f, 0f);
            }
            else
            {
                child[i].transform.Translate(-x_spostamento * speed_x * Time.deltaTime, 0f, 0f);
            }
            yield return null;
        }
        yield return null;
    }
}
