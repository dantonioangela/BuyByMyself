using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    /*[SerializeField] private float _mouseSensitivity = 100f;
    private float _speed = 10f;
    private float angularSpeed = 5f;
    //[SerializeField] private float _gravity = -9.81f;
    

    
    private Rigidbody rb;

    private float mouseX;
    private float mouseY;

    private float h;
    private float v;
    public Camera camera;

    Vector3 move;
    private float cameraXRotation = 0f;
    */
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

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        carrelloCollider = GetComponent<BoxCollider>();
        /*Cursor.lockState = CursorLockMode.Locked;
        
        rb = GetComponent<Rigidbody>();*/
    }

    // Update is called once per frame
    void Update()
    {
        if(carrello.mode == 0)
        {
            carrelloCollider.enabled = true;
        }
        else if (carrello.mode == 1)
        {
            carrelloCollider.enabled = false;
        }


        /*
        UpdateCursor();

        if (Cursor.lockState == CursorLockMode.None)
            return;

        mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        //Compute direction According to Camera Orientation
        //transform.Rotate(Vector3.up, mouseX);
        rb.MoveRotation(Quaternion.Euler(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y + mouseX * angularSpeed *Time.deltaTime, rb.transform.eulerAngles.z));
        cameraXRotation -= mouseY;
        cameraXRotation = Mathf.Clamp(cameraXRotation, -90f, 90f);
        camera.transform.localRotation = Quaternion.Euler(cameraXRotation, 0f, 0f);


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rb.velocity = (rb.position + ((transform.right * h + transform.forward * v).normalized * _speed * Time.deltaTime));
        //transform.localPosition += (transform.right * h + transform.forward * v).normalized * _speed * Time.deltaTime;
        //transform.localPosition += new Vector3(xPos, 0, zPos);
        //transform.position += new Vector3(xPos, 0, yPos);
        //transform.position = new Vector3(xPos, 0, yPos);
        //move = (transform.right * h + transform.forward * v).normalized;
        //transform.Translate(move * _speed * Time.deltaTime);
        //_characterController.Move(move * _speed * Time.deltaTime);
    }

    private void UpdateCursor()
    {
        if (Cursor.lockState == CursorLockMode.None && Input.GetMouseButtonDown(1))
            Cursor.lockState = CursorLockMode.Locked;

        if (Cursor.lockState == CursorLockMode.Locked && Input.GetKeyDown(KeyCode.Escape))
            Cursor.lockState = CursorLockMode.None;*/

        playerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        playerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        MovePlayer();
        MoveCamera();
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
        transform.Rotate(0f, playerMouseInput.x * sensitivityX, 0f);
        //playerRB.angularVelocity = new Vector3(0f, playerMouseInput.x * sensitivity, 0f);
        playerCamera.transform.localRotation = Quaternion.Euler(xRot , 0f, 0f);
        transform.eulerAngles = new Vector3(0f, transform.eulerAngles.y, 0f);
        transform.position = new Vector3(transform.position.x, 1.65f, transform.position.z);
    }
        
}
