using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPSInteractionManager : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    private float _interactionDistance = 7;

    [SerializeField] private Image _target;

    private Interactable _pointingInteractable;
    private Grabbable _pointingGrabbable;

    private CapsuleCollider playerCollider;
    private Player_Controller playerController;
    private Vector3 _rayOrigin;

    private Grabbable _grabbedObject = null;
    private Ray ray;
    private RaycastHit hit;

    [SerializeField] private Sprite _targetDefaultIcon;
    [SerializeField] private Sprite _targetGrabIcon;
    
   


    void Start()
    {
        _interactionDistance = 7;
        _grabbedObject = null;

        playerCollider = GetComponent<CapsuleCollider>();
        playerController = GetComponent<Player_Controller>();
    }

    void Update()
    {
        //_rayOrigin = _fpsCameraT.position + playerCollider.radius * _fpsCameraT.forward;
        UpdateUITarget();
        if (!Player_Controller.UI_active)
        {
            if (_grabbedObject == null)
                CheckInteraction();

            if (_grabbedObject != null && Input.GetMouseButtonUp(0))
                Drop();

            if (_debugRay)
                DebugRaycast();
        }
    }

    private void CheckInteraction()
    {
        //Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            //Check if is interactable
            _pointingInteractable = hit.transform.GetComponent<Interactable>();
            if (_pointingInteractable)
            { 
                if(Input.GetMouseButtonDown(0))
                    _pointingInteractable.Interact(gameObject);
            }

            //Check if is grabbable
            _pointingGrabbable = hit.transform.GetComponent<Grabbable>();
            if (_grabbedObject == null && _pointingGrabbable)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _pointingGrabbable.Grab(gameObject);
                    Grab(_pointingGrabbable);
                }
                    
            }
        }
        //If NOTHING is detected set all to null
        else
        {
            _pointingInteractable = null;
            _pointingGrabbable = null;
        }
    }

    private void UpdateUITarget()
    {
        //if (_pointingInteractable)
        //    _target.color = Color.green;
        if(Player_Controller.UI_active){
            _target.enabled = false;
        }
        else{
            _target.enabled = true;
            if (_pointingGrabbable)
                _target.sprite = _targetGrabIcon;
            else
                _target.sprite = _targetDefaultIcon;
        }
        
    }

    private void Drop()
    {
        if (_grabbedObject == null)
            return;

        _grabbedObject.transform.parent = _grabbedObject.OriginalParent;
        _grabbedObject.Drop();

        _target.enabled = true;
        _grabbedObject = null;
    }

    private void Grab(Grabbable grabbable)
    {
        _grabbedObject = grabbable;
        grabbable.transform.SetParent(_fpsCameraT, true);

        _target.enabled = false;
    }

    private void DebugRaycast()
    {
        Debug.DrawRay(_rayOrigin, _fpsCameraT.forward * _interactionDistance, Color.red);
    }
}
