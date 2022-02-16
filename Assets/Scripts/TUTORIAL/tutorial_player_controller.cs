using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_player_controller : MonoBehaviour
{
    public bool inventario;
    public bool UI_active;
    private Vector3 playerMovementInput;
    public static bool tutorialStepBananeStart;
    public bool tutorialStepInventarioStart;
    public bool tutorialStepOpenInventarioDone;
    private Vector2 playerMouseInput;
    public tutorial_canvas_controller speech;
    public tutorial_inventario tutorialInventario;

    private Transform playerCamera;
    private Rigidbody playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private float xRot;
    //public Carrello_controller carrello;
    [System.NonSerialized] public tutorial_carrello_controller carrello;
    private Collider carrelloCollider;

    private Vector3 moveVector;

    void Start()
    {
        UI_active = false;
        tutorialStepBananeStart = false;
        tutorialStepInventarioStart = false;
        tutorialStepOpenInventarioDone = false;
        playerCamera = transform.GetChild(0);
        carrello = transform.GetChild(1).GetComponent<tutorial_carrello_controller>();
        Cursor.lockState = CursorLockMode.Locked;
        playerRB = GetComponent<Rigidbody>();
        carrelloCollider = GetComponent<BoxCollider>();
        inventario = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventario && !UI_active)
        {
            Cursor.lockState = CursorLockMode.Locked;
            if (tutorial_carrello_controller.mode == 0)
            {
                carrelloCollider.enabled = true;

            }
            else if (tutorial_carrello_controller.mode == 1)
            {
                carrelloCollider.enabled = false;
            }

            playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            MovePlayer();
            MoveCamera();
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            playerRB.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    private void MovePlayer()
    {
        if (tutorial_carrello_controller.mode == 1)
        {
            moveVector = transform.TransformDirection(playerMovementInput) * speed * 1.5f;
        }
        else
        {
            moveVector = transform.TransformDirection(playerMovementInput) * speed;
        }
        playerRB.velocity = new Vector3(moveVector.x, 0f, moveVector.z);
    }

    private void MoveCamera()
    {
        if (!UI_active)
        {
            xRot -= playerMouseInput.y * sensitivityY;
            xRot = Mathf.Clamp(xRot, -10f, 20f);
            if (tutorial_carrello_controller.mode == 0 && xRot > 18f && tutorialStepBananeStart)
            {
                carrello.GetComponent<isSelectable>().Select();
                tutorial_carrello_controller.selected = true;
                if (Input.GetMouseButtonDown(0) && tutorialStepInventarioStart)
                {
                    inventario = true;
                    if (!tutorialStepOpenInventarioDone)
                    {
                        speech.ChangeSpeech(10);
                        tutorialStepOpenInventarioDone = true;
                    }
                    tutorialInventario.tutorialStepViaBananeStart = true;
                }
            }
            else if (xRot <= 18f && tutorial_carrello_controller.selected && tutorialStepBananeStart)
            {
                carrello.GetComponent<isSelectable>().Deselect();
                tutorial_carrello_controller.selected = false;

            }
            transform.Rotate(0f, playerMouseInput.x * sensitivityX, 0f);
            playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
            transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
        }
    }

    public void SetInventario(bool status)
    {
        inventario = status;
    }
    public void setUI( bool val)
    {
        UI_active = val;
    }
}
