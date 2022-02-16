using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class tutorial_grabbable : Grabbable
{
    private Collider _collider;
    private Rigidbody _rigidBody;
    private Vector3 originalPosition;
    private Quaternion originalRotation;


    protected override void Start()
    {
        base.Start();
        _collider = GetComponent<Collider>();
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;
        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
    }

    public override void Grab(GameObject grabber)
    {
        _collider.enabled = false;
    }

    public override void Drop()
    {
        gameObject.transform.position = originalPosition;
        gameObject.transform.rotation = originalRotation;
        if (tutorial_carrello_controller.selected)
        {
            gameObject.transform.position -= new Vector3(0f, 10f, 0f);
            //GetComponent<Product>().transform.SetParent(Camera.main.gameObject.transform.parent.GetChild(1));
            Camera.main.gameObject.transform.parent.GetComponent<tutorial_player_controller>().carrello.GetComponent<tutorial_carrello_controller>().AddProductToChart(gameObject);
        }
        else
        {
            _collider.enabled = true;
        }
    }
}
