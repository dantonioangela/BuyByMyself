#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProductModel {
    //public string id { get; }
    public string name;
    public string listName;
    public bool? sustainable;
    public bool? packaging;
    public string size;
    public float? origin;
    public List<int> season;
    public float price;
    public int counter; //numero dei modelli di questo tipo necessari nella scena

    public ProductModel(string name, string listName, bool? sustainable, bool? packaging, string size, float? origin, List<int> season, float price)
    {
        this.listName = listName;
        this.name = name;
        this.sustainable = sustainable;
        this.packaging = packaging;
        this.size = size;
        this.origin = origin;
        this.season = season;
        this.price = price;
        this.counter = 0;
    }

    public void resetCounter()
    {
        this.counter = 0;
    }
}
