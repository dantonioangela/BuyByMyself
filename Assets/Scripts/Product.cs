using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

class Product : PhysicsGrabbable {

    private ProductModel model;
    public int quality { get; }
    public  DateTime expirationDate { get; }

    protected override void Start() {
        base.Start();
        //TODO: generare qualità e data di scadenza in base a determinati parametri
    }
    
}

