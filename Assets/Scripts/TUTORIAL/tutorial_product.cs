using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_product : tutorial_grabbable
{
    public new string name;
    public string listName;
    public bool? sustainable;
    public bool? packaging;
    public int? size;
    public int? origin;
    public int[] season = new int[4];
    public float price;
    public int counter;
    public int quality;
    public int expirationDate;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        //TODO: generare qualità e data di scadenza in base a determinati parametri
    }

}
