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
    public static List<ProductModel> productModels;
    public static Dictionary<string, int[]> modelsAvailability; //nome + numProdottiFullStats + numTotaleProdotti con   quel nome
    public static Dictionary<string, int[]> NamesToIndex;       //int[0] è l'indice della prima occorrenza e int[1] è il numero di elelementi con nome = key

    private String xmlPath;
    private XmlTextReader reader; 
    bool finishedLoading = false;

    void Awake() {
        productModels = new List<ProductModel>();
        modelsAvailability = new Dictionary<string, int[]>();
        NamesToIndex = new Dictionary<string, int[]>();

        //xmlPath = "Assets/Resources/product_models.xml";
        xmlPath = "Assets/Resources/prova.xml";
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
        XDocument xml = XDocument.Load(xmlPath);

        // Query the data and write out a subset of contacts
        var products = from product in xml.Descendants("product")
            select new {
                //xmlId = product.Element("id").Value,
                xmlName = product.Element("name").Value,
                xmlListName = product.Element("listname").Value.ToLower(),
                xmlSustainable = product.Element("sustainable").Value,
                xmlPackaging = product.Element("packaging").Value,
                xmlSize = product.Element("size").Value,
                xmlOrigin = product.Element("origin").Value,
                xmlSeason = product.Element("season").Value,
                xmlPrice = product.Element("price").Value
            };
        int i = 0;

        bool? sustainable;
        bool? packaging;
        int? size;
        int? origin;
        int[]? season;
        string nomeLista;
        string nomeListaBefore = " ";
        float price;
        int[] elemento = new int[2];
        foreach (var product in products) {
            sustainable = ToNullableBool(product.xmlSustainable);
            packaging = ToNullableBool(product.xmlPackaging);
            size = ToNullableInt(product.xmlSize);
            origin = ToNullableInt(product.xmlOrigin);
            season = ToNullableIntArray(product.xmlSeason);
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
        string[] lines = System.IO.File.ReadAllLines("Assets/Resources/ProductsAvailability.txt");
        foreach(string line in lines){
            string[] strings = line.Split('/');
            name = strings[0].ToLower();
            string[] substrings = strings[1].Split(' ');
            modelsAvailability.Add(name, new int[2] { int.Parse(substrings[0]), int.Parse(substrings[1]) });
        }
    }

}
