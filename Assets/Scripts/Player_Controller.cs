using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public bool inventario;
    public bool UI_active = false;
    private Vector3 playerMovementInput;
    private Vector2 playerMouseInput;

    [SerializeField] private Transform playerCamera;
    private Rigidbody playerRB;
    [SerializeField] private float speed;
    [SerializeField] private float sensitivityX;
    [SerializeField] private float sensitivityY;
    private float xRot;
    public Carrello_controller carrello;
    private Collider carrelloCollider;

    private Vector3 moveVector;

    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        carrelloCollider = GetComponent<BoxCollider>();
        inventario = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!inventario && !UI_active)
        {
            if (carrello.mode == 0)
            {
                carrelloCollider.enabled = true;

            }
            else if (carrello.mode == 1)
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
            playerRB.velocity = new Vector3(0f, 0f, 0f);
        }
    }

    private void MovePlayer()
    {
        if (carrello.mode == 1)
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
        xRot -= playerMouseInput.y * sensitivityY ;
        xRot = Mathf.Clamp(xRot, -10f, 20f);
        if (carrello.mode == 0 && xRot > 18f)
        {
            carrello.GetComponent<isSelectable>().Select();
            if (Input.GetMouseButtonDown(0))
            {
                inventario = true;
            }
        }
        else if( !carrello.selected )
        {
            carrello.GetComponent<isSelectable>().Deselect();
        }
        transform.Rotate(0f, playerMouseInput.x * sensitivityX, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot , 0f, 0f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
    }

    public void SetInventario( bool status)
    {
        inventario = status;
    }
        
}
