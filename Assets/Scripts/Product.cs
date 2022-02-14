using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Product : PhysicsGrabbable {

    public ProductModel model;
    public int quality { get; }
    [HideInInspector]
    public bool expirated;
    private ProductLabel productLabel;
    public  Vector3 position;
    public Quaternion rotation;

    protected override void Start() {
        base.Start();
        productLabel = FindObjectOfType<ProductLabel>();
        position = transform.position;
        rotation = transform.rotation;
    }
	
    public override void Grab(GameObject grabber)
    {
        base.Grab(grabber);
        productLabel.active = true;
        productLabel.product = this;
    }

    public override void Drop()
    {
        base.Drop();
        productLabel.active = false;
    }
	
}

