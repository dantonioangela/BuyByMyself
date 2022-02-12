using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_vetro_controller : MonoBehaviour
{
    private GameObject vetro;
    private Vector3 pos_init;
    private float z_spostamento = 0.002f;
    private float x_spostamento;
    private bool isSelected;
    private int aperto;
    private Ray ray;
    private RaycastHit hit;
    private float speed_z = 1f;
    private float speed_x = 3f;

    public bool tutorialStepVetroStart = false;
    private bool tutorialVetroEnabled = false;
    public bool tutorialStepVetroDone = false;
    public bool tutorialStepSalmoneStart = false;
    public bool tutorialStepSalmoneDone = false;
    private bool alreadyEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        isSelected = false;
        aperto = 0;

        vetro = transform.GetChild(1).gameObject;

        pos_init = vetro.transform.localPosition;
        x_spostamento = transform.GetChild(0).localPosition.x - pos_init.x;

    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialStepVetroDone && tutorialStepSalmoneStart && !alreadyEnabled)
        {
            transform.GetChild(3).GetComponent<MeshCollider>().enabled = true;
            transform.GetChild(3).GetComponent<tutorial_product>().enabled = true;
            alreadyEnabled = true;
        }
        if (tutorialStepVetroStart)
        {
            if (!tutorialVetroEnabled)
            {
                vetro.GetComponent<BoxCollider>().enabled = true;
                vetro.GetComponent<isSelectable>().enabled = true;
            }
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5.0f))
            {
                if (hit.collider == vetro.GetComponent<Collider>())
                {
                    if (!isSelected)
                    {
                        isSelected = true;
                        vetro.GetComponent<isSelectable>().Select();
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        aperto++;
                        if (aperto == 1)
                        {
                            StartCoroutine(ToLeft());
                            tutorialStepVetroDone = true;
                            tutorialStepSalmoneStart = true;
                        }
                        if (aperto == 2)
                        {
                            StartCoroutine(GoBack());
                        }
                    }
                }
                else if (isSelected)
                {
                    vetro.GetComponent<isSelectable>().Deselect();
                    isSelected = false;
                }
            }
            else if (isSelected)
            {
                vetro.GetComponent<isSelectable>().Deselect();
                isSelected = false;
            }
        }
    }

    private IEnumerator ToLeft()
    {
        while (vetro.transform.localPosition.y > pos_init.y - z_spostamento)
        {
            vetro.transform.position += vetro.transform.up * Time.deltaTime * speed_z;
            yield return new WaitForEndOfFrame();
        }
        while (vetro.transform.localPosition.x < pos_init.x + x_spostamento)
        {
            vetro.transform.position -= -vetro.transform.right * Time.deltaTime * speed_x;
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    private IEnumerator GoBack()
    {
        while (vetro.transform.localPosition.x > pos_init.x)
        {
            vetro.transform.position += -vetro.transform.right * Time.deltaTime * speed_x;
            yield return new WaitForEndOfFrame();
        }
        while (vetro.transform.localPosition.y < pos_init.y)
        {
            vetro.transform.position -= vetro.transform.up * Time.deltaTime * speed_z;
            yield return new WaitForEndOfFrame();
        }
        aperto = 0;
        yield return null;
    }
}
