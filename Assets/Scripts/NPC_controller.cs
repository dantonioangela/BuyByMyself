using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPC_controller : MonoBehaviour
{
    public NavMeshAgent myAgent;
    private GameObject[] availablePlaces;
    public Player_Controller player;
    private Animator animator;
    private int lastShelfChosen;
    private Ray ray;
    private RaycastHit hit;
    private Ray rayCam;
    private RaycastHit hitCam;
    private bool alreadyWaiting = false;
    private int seed = 4679;
    private float smooth = 0.3f;
    private bool isWalking;
    public float animationSpeed;
    public int animationCounter = 1;
    private string triggerName;
    public bool isAnimatedClick = false;

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
        if (animator.GetCurrentAnimatorStateInfo(0).IsName(animationCounter.ToString()) || animator.GetCurrentAnimatorStateInfo(0).IsName(animationCounter.ToString() + " 0") || animator.GetCurrentAnimatorStateInfo(0).IsName(animationCounter.ToString() + " 1"))
        {
            myAgent.velocity = Vector3.zero;
            animator.speed = 1f;
        }
        else
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


                rayCam = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(rayCam, out hitCam, 10.0f))
                {
                    if (Input.GetMouseButtonDown(0) && hitCam.collider.Equals( myAgent.gameObject.GetComponent<Collider>() ) )
                    {
                        isAnimatedClick = true;
                        StartCoroutine(RotateMeToPlayer( Quaternion.LookRotation(player.transform.position - transform.position, Vector3.up )));
                        triggerName = "Trigger" + animationCounter;
                        
                        animator.SetTrigger(triggerName);
                    }
                }
            }
            else
            {
                animator.speed = 0f;
                myAgent.isStopped = true;
            }
        }
    }

    IEnumerator Waiter()
    {
        alreadyWaiting = true;
        animator.SetBool("isWalking", false);

        animator.speed = 1;
        isWalking = false;
        StartCoroutine(RotateMeToShelf(availablePlaces[lastShelfChosen].transform.rotation));
        yield return new WaitForSecondsRealtime(Random.Range(3, 7));
        animator.SetBool("isWalking", true);
        isWalking = true;
        SetDestination();
        alreadyWaiting = false;
    }

    IEnumerator RotateMeToShelf( Quaternion rot)
    {
        float slider = 0;
        while (slider < 1) 
        {
            slider += Time.deltaTime * smooth;

            if (!alreadyWaiting)
            {
                yield break;
            }
            if(! isAnimatedClick) transform.rotation = Quaternion.Slerp(transform.rotation, rot, slider);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    IEnumerator RotateMeToPlayer(Quaternion rot)
    {
        float slider = 0;
        while (slider < 1)
        {
            slider += Time.deltaTime * smooth;

            if (!isAnimatedClick)
            {
                yield break;
            }
            transform.rotation = Quaternion.Slerp(transform.rotation, rot, slider);
            yield return new WaitForEndOfFrame();
        }
        yield return null;
    }

    void SetDestination()
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