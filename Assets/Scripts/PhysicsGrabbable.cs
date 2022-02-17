using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class PhysicsGrabbable : Grabbable
{
    private Collider _collider;
    private Rigidbody _rigidBody;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float timeLeft = 30; //tempo prima che l'oggetto torni nella posizione originale
    private bool dropped = false;

    protected override void Start ()
    {
        base.Start();
        timeLeft = 30;
        dropped = false;
        _collider = gameObject.AddComponent(typeof(BoxCollider)) as BoxCollider;
        _rigidBody = gameObject.AddComponent(typeof(Rigidbody)) as Rigidbody;
        _rigidBody.isKinematic = true;
        _collider.enabled = true;

        originalPosition = gameObject.transform.position;
        originalRotation = gameObject.transform.rotation;
    }

    protected void Update()
    {
        if (dropped)
        {
            timeLeft -= Time.deltaTime;
            if (timeLeft < 0) {
                gameObject.transform.position = originalPosition;
                gameObject.transform.rotation = originalRotation;
                _collider.enabled = true;
				_rigidBody.isKinematic = true;							  
                dropped = false;
                timeLeft = 10;
            } 
        }
    }

    public override void Grab(GameObject grabber)
    {
		_collider.enabled = false;						  
        if (dropped)
        {
			_rigidBody.isKinematic = true;							  
            dropped = false;
            timeLeft = 10;
        }

    }

    public override void Drop()
    {        
								 
        if ( Carrello_controller.selected && gameObject.GetComponent<Product>() != null)
        {
            gameObject.transform.position -= new Vector3(0f, 10f, 0f);
            Camera.main.gameObject.transform.parent.GetComponent<Player_Controller>().carrello.GetComponent<Carrello_controller>().AddProductToChart( gameObject.GetComponent<Product>() );
        }
        else{
            _collider.enabled = true;
            dropped = true;
            _rigidBody.isKinematic = false;
        }
    }
}
