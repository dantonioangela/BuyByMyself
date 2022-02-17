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

    public static List<ProductModel> productModels = new List<ProductModel>();
    public static Dictionary<string, int[]> modelsAvailability = new Dictionary<string, int[]>(); //nome + numProdottiFullStats + numTotaleProdotti con   quel nome
    public static Dictionary<string, int[]> NamesToIndex = new Dictionary<string, int[]>();       //int[0] è l'indice della prima occorrenza e int[1] è il numero di elelementi con nome = key

    //private String xmlPath = "Assets/Resources/prova.xml";
    private string xmlPath;
    //private XmlTextReader reader; 

    void Start() {

    }

    void Update() {

    }

    void Awake()
    {
        xmlPath = Path.Combine(Application.streamingAssetsPath, "product_models.xml");
        LoadXML();
        createDictionary();
        ListaSpesa.InitList();
    }

    void LoadXML()
    {
        // Loading from a file, you can also load from a stream
        XDocument xml = XDocument.Load(xmlPath);

        if (productModels.Count > 0)
        {
            productModels.Clear();
        }
        if (NamesToIndex.Count > 0)
        {
            NamesToIndex.Clear();
        }
        // Query the data and write out a subset of contacts
        var products = from product in xml.Descendants("product")
            select new {
                xmlName = product.Element("name").Value,
                xmlListName = product.Element("listname").Value.ToLower(),          //  frutta/banana_bad
                xmlSustainable = product.Element("sustainable").Value,              //  frutta/banana
                xmlPackaging = product.Element("packaging").Value,                  //  fette biscottate/integrali_eco
                xmlSize = product.Element("size").Value,
                xmlOrigin = product.Element("origin").Value,
                xmlSeason = product.Element("season").Value,
                xmlPrice =  product.Element("price").Value
    };
        int i = 0;
        bool? sustainable;
        bool? packaging;
        string size;
        float? origin;
        List<int> season;
        string nomeLista;
        string nomeListaBefore = " ";
        float price;
        int[] elemento = new int[2];

        foreach (var product in products) {
            sustainable = ToNullableBool(product.xmlSustainable);
            packaging = ToNullableBool(product.xmlPackaging);
            size = (product.xmlSize == "null" ? "" : product.xmlSize);
            origin = ToNullableFloat(product.xmlOrigin);
            season = product.xmlSeason.Equals("null") ? null : product.xmlSeason.Split(' ').Select(n => Convert.ToInt32(n)).ToList();
            price = float.Parse(product.xmlPrice);


            nomeLista = product.xmlListName;
            if (nomeLista != nomeListaBefore)
            {
                if (i != 0 )
                {
                    elemento[1] = i - elemento[0];
                    NamesToIndex.Add(nomeListaBefore, new int[2] { elemento[0], elemento[1] });
                }
                nomeListaBefore = nomeLista;
                elemento[0] = i;
            }
            if (i == (products.Count() - 1))
            {
                elemento[1] = i - elemento[0] + 1;
                NamesToIndex.Add(nomeListaBefore, new int[2] { elemento[0], elemento[1] });
            }
            productModels.Add(new ProductModel(product.xmlName, nomeLista, sustainable, packaging, size, origin, season, price));
            i++;
        }
    }

    private int? ToNullableInt(string str) {
        int temp;
        if (int.TryParse(str, out temp))
            return temp;
        return null;
    }
    private float? ToNullableFloat(string str)
    {
        float temp;
        if (float.TryParse(str, out temp))
            return temp;
        return null;
    }

    private bool? ToNullableBool(string str) {
        if (str == "0")
            return false;
        if (str == "1")
            return true;
        return null;
    }

    private int[]? ToNullableIntArray(string str) {
        if (str != "null") return str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        return null;
    }

    void createDictionary(){
        if (modelsAvailability.Count > 0)
        {
            modelsAvailability.Clear();
        }
        string name;
        string[] lines = System.IO.File.ReadAllLines(Path.Combine(Application.streamingAssetsPath, "ProductsAvailability.txt"));
        foreach(string line in lines){
            string[] strings = line.Split(':');
            name = strings[0].ToLower();
            string[] substrings = strings[1].Split(' ');
            modelsAvailability.Add(name, new int[2] { int.Parse(substrings[0]), int.Parse(substrings[1]) });
        }
    }

}