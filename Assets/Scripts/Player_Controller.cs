using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public AudioManager audioMan;
    public bool inventario;
    static public bool UI_active = false;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;

    private Transform playerCamera;
    private Rigidbody playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private float xRot;
    //public Carrello_controller carrello;
    [System.NonSerialized] public Carrello_controller carrello;
    private Collider carrelloCollider;

    private Vector3 moveVector;

    void Start()
    {
        UI_active = false;
        playerCamera = transform.GetChild(0);
        carrello = transform.GetChild(1).GetComponent<Carrello_controller>();
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
            if (Carrello_controller.mode == 0)
            {
                carrelloCollider.enabled = true;

            }
            else if (Carrello_controller.mode == 1)
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
        if (Carrello_controller.mode == 1)
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
        xRot -= playerMouseInput.y * sensitivityY;
        xRot = Mathf.Clamp(xRot, -10f, 20f);
        if (Carrello_controller.mode == 0 && xRot > 18f)
        {
            carrello.GetComponent<isSelectable>().Select();
            Carrello_controller.selected = true;
            if (Input.GetMouseButtonDown(0))
            {
                
                inventario = true;
                UI_active = true;
            }
        }
        else if (xRot <= 18f && Carrello_controller.selected)
        {
            carrello.GetComponent<isSelectable>().Deselect();
            Carrello_controller.selected = false;
        }
        transform.Rotate(0f, playerMouseInput.x * sensitivityX, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
    }

    public void CloseInventario()
    {
        inventario = false;
        UI_active = false;
    }

    public void setUIactive()
    {
        UI_active = true;
    }

}
