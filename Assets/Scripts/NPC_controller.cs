using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_controller : MonoBehaviour
{
    private NavMeshAgent myAgent;
    private GameObject[] availablePlaces;
    public Player_Controller player;
    private Animator animator;
    private int lastShelfChosen;
    private Ray ray;
    private RaycastHit hit;
    private bool alreadyWaiting = false;
    private int seed = 1900;
    private float smooth = 5.0f;
    private bool isWalking;
    public float animationSpeed;

    private void Awake()
    {
        UnityEngine.Random.InitState(seed);
    }

    // Start is called before the first frame update
    private void Start()
    {
        availablePlaces = GameObject.FindGameObjectsWithTag("PlaceToStop");
        lastShelfChosen = Random.Range(0, availablePlaces.Length - 1);
        myAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        animator.SetBool("isWalking", true);
        isWalking = true;
        SetDestination();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!player.inventario)
        {
            if (myAgent.isStopped)
            {
                myAgent.isStopped = false;
                if (!isWalking) animator.speed = 1;
            }
            ray = new Ray(transform.position, Vector3.forward);
            if (DestinationReached() && !alreadyWaiting)
            {
                StartCoroutine(Waiter());
            }
            else if (Physics.Raycast(ray, out hit, 0.9f))
            {
                if (hit.transform.gameObject.tag == "NPC" && hit.transform != transform && hit.normal.magnitude - myAgent.transform.forward.magnitude < 0.1 && hit.normal.magnitude - myAgent.transform.forward.magnitude > -0.1)
                {
                    SetDestination();
                }
            }
            if (isWalking) animator.speed = myAgent.velocity.magnitude * animationSpeed;
        }
        else
        {
            animator.speed = 0f;
            myAgent.isStopped = true;
        }
    }

    IEnumerator Waiter()
    {
        alreadyWaiting = true;
        animator.SetBool("isWalking", false);

        animator.speed = 1;
        isWalking = false;
        StartCoroutine(RotateMe());
        yield return new WaitForSecondsRealtime(Random.Range(3, 7));
        animator.SetBool("isWalking", true);
        isWalking = true;
        SetDestination();
        alreadyWaiting = false;
    }

    IEnumerator RotateMe()
    {
        while (transform.rotation != availablePlaces[lastShelfChosen].transform.rotation)
        {
            if (!alreadyWaiting)
            {
                yield break;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, availablePlaces[lastShelfChosen].transform.rotation, Time.deltaTime * smooth);
            yield return null;
        }
    }

    private void SetDestination()
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 randomPosition = FindRandomPosition();
        while (!myAgent.CalculatePath(randomPosition, path))
        {
            randomPosition = FindRandomPosition();
        }
        myAgent.SetDestination(randomPosition);
    }

    private bool DestinationReached()
    {
        if (!myAgent.pathPending)
        {
            if (myAgent.remainingDistance < myAgent.stoppingDistance)
            {
                if (!myAgent.hasPath || myAgent.velocity.sqrMagnitude <= 0f)
                {
                    return true;
                }
            }
        }
        return false;
    }

    private Vector3 FindRandomPosition()
    {
        int shelfIndex = Random.Range(0, availablePlaces.Length);
        while (shelfIndex == lastShelfChosen)
        {
            shelfIndex = Random.Range(0, availablePlaces.Length - 1);
        }
        lastShelfChosen = shelfIndex;
        GameObject ChosenShelf = availablePlaces[shelfIndex];
        Collider ChosenShelfCollider = ChosenShelf.GetComponent<Collider>();
        Vector3 min = ChosenShelfCollider.bounds.min;
        Vector3 max = ChosenShelfCollider.bounds.max;
        return new Vector3(Random.Range(min.x, max.x), 0f, Random.Range(min.z, max.z));
    }
}