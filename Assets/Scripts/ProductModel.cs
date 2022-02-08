#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class ProductModel {
    public string id { get; }
    public string name { get; }
    public bool? sustainable { get; }
    public bool? packaging { get; }
    public int? size { get; }
    public int? origin { get; }
    public int[]? season { get; }
    public float price { get; }

    public ProductModel(string id, string name, bool? sustainable, bool? packaging, int? size, int? origin, int[]? season, float price)
    {
        this.id = id;
        this.name = name;
        this.sustainable = sustainable;
        this.packaging = packaging;
        this.size = size;
        this.origin = origin;
        this.season = season;
        this.price = price;
    }
}
