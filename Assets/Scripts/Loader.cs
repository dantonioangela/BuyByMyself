#nullable enable

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Xml.Linq;
using System.IO;
using UnityEngine.SceneManagement;

class Loader : MonoBehaviour
{
    public static ArrayList productModels;
    public static Dictionary<string, int> modelsAvailability;
    private String xmlPath;
    private XmlTextReader reader; 
    bool finishedLoading = false;

    void Start() {
        productModels = new ArrayList();
        modelsAvailability = new Dictionary<string, int>();
        xmlPath = "Assets/Resources/product_models.xml";
        reader = new XmlTextReader(xmlPath);
        LoadXML();  
        createDictionary();
    }

    void Update() {
        if (finishedLoading) {
            
        }

    }

    void LoadXML()
    {
        // Loading from a file, you can also load from a stream
        XDocument xml = XDocument.Load("Assets/Resources/product_models.xml");

        // Query the data and write out a subset of contacts
        var products = from product in xml.Descendants("product")
            select new {
                xmlId = product.Element("id").Value,
                xmlName = product.Element("name").Value,
                xmlSustainable = product.Element("sustainable").Value,
                xmlPackaging = product.Element("packaging").Value,
                xmlSize = product.Element("size").Value,
                xmlOrigin = product.Element("origin").Value,
                xmlSeason = product.Element("season").Value,
                xmlPrice = product.Element("price").Value
            };
 
        foreach (var product in products) {
            bool? sustainable = ToNullableBool(product.xmlSustainable);
            bool? packaging = ToNullableBool(product.xmlPackaging);
            int? size = ToNullableInt(product.xmlSize);
            int? origin = ToNullableInt(product.xmlOrigin);
            int[]? season = ToNullableIntArray(product.xmlSeason);
            float price = float.Parse(product.xmlPrice);
            productModels.Add(new ProductModel(product.xmlId, product.xmlName, sustainable, packaging, size, origin, season, price));
        }

        finishedLoading = true; //tell the program that we’ve finished loading data. yield return null;

    }

    private int? ToNullableInt(string str) {
        int temp;
        if (int.TryParse(str, out temp)) return temp;
        return null;
    }

    private bool? ToNullableBool(string str) {
        bool temp;
        if (bool.TryParse(str, out temp)) return temp;
        return null;
    }

    private int[]? ToNullableIntArray(string str) {
        if (str != "null") return str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        return null;
    }

    void createDictionary(){
        string name;
        int quantity;
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/ProductsAvailability.txt");
        foreach(string line in lines){
            string[] strings = line.Split(' ');
            modelsAvailability.Add(strings[0], int.Parse(strings[1]));
        }
    }

}
