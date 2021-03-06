using UnityEngine;
using UnityEngine.UI;

public class tutorial_FPS : MonoBehaviour
{
    [SerializeField] private Transform _fpsCameraT;
    [SerializeField] private bool _debugRay;
    [SerializeField] private float _interactionDistance;

    [SerializeField] private Image _target;

    private Interactable _pointingInteractable;
    private Grabbable _pointingGrabbable;

    private CapsuleCollider playerCollider;
    private tutorial_player_controller playerController;
    private Vector3 _rayOrigin;
    private Ray ray;

    private Grabbable _grabbedObject = null;

    [SerializeField] private Sprite _targetDefaultIcon;
    [SerializeField] private Sprite _targetGrabIcon;



    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider>();
        playerController = GetComponent<tutorial_player_controller>();
    }

    void Update()
    {
        //_rayOrigin = _fpsCameraT.position + playerCollider.radius * _fpsCameraT.forward;

        if (_grabbedObject == null)
            CheckInteraction();

        if (_grabbedObject != null && Input.GetMouseButtonUp(0))
            Drop();

        UpdateUITarget();

        if (_debugRay)
            DebugRaycast();
    }

    private void CheckInteraction()
    {
        //Ray ray = new Ray(_rayOrigin, _fpsCameraT.forward);
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, _interactionDistance))
        {
            //Check if is interactable
            _pointingInteractable = hit.transform.GetComponent<Interactable>();
            if (_pointingInteractable)
            {
                if (Input.GetMouseButtonDown(0))
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
        if (tutorial_player_controller.UI_active)
        {
            _target.enabled = false;
        }
        else
        {
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
