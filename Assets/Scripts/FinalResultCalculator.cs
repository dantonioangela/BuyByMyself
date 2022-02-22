using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result
{
    public float totalPoints;
    public float? qualityPoints;
    public float? ecoPoints; //packaging
    public float? sustainablePoints;
    public float? originPoints;
    public float? seasonPoints;
    public float pricePoints;


    public Result(float totalPoints,
                  float? qualityPoints,
                  float? ecoPoints,
                  float? originPoints,
                  float? sustainablePoints,
                  float? seasonPoints,
                  float pricePoints)
    {
        this.totalPoints = totalPoints;
        this.qualityPoints = qualityPoints;
        this.ecoPoints = ecoPoints;
        this.sustainablePoints = sustainablePoints;
        this.originPoints = originPoints;
        this.seasonPoints = seasonPoints;
        this.pricePoints = pricePoints;
    }
}
public class FinalResultCalculator
{
    public static Result calculateFinalResult(List<Product> cartList, int levelDifficuly)
    {


    //90% dei punti sono dati dai prodotti, 10% dal prezzo finale della spesa
    //i punti del prezzo finale vengono dati solo se sono presenti nel carrello 
    //tutti i prodotti richiesti nella lista della spesa, almeno nella quantità indicata
    //quindi non vengono dati se mancano dei prodotti

    float maxPricePoints = 10; //massimo punteggio che si può ottenere valutando il prezzo della spesa complessiva

        int totalShoppingListCount = 0; //numero total di prodotti richiesti nella lista
        foreach(var entry in ListaSpesa.listaSpesa)
            totalShoppingListCount += entry.Value;

        float maxProductPoints = (100 - maxPricePoints) / totalShoppingListCount; //massimo punteggio che un singolo prodotto può dare

        float totalPoints = 0;

        int qualityProducts = 0; //numero di prodotti nella lista che hanno il campo qualità (frutta e verdura)
        float qualityPoints = 0;
        float? finalQualityPoints;

        int ecoProducts = 0; //numero di prodotti nella lista che hanno la versione eco
        float ecoPoints = 0;
        float? finalEcoPoints;

        int originProducts = 0; //numero di prodotti nella lista che hanno origine
        float originPoints = 0;
        float? finalOriginPoints;

        int sustainableProducts = 0; //numero di prodotti nella lista che hanno la versione sostenibile
        float sustainablePoints = 0;
        float? finalSustainablePoints;

        int seasonProducts = 0; //numero di prodotti nella lista che sono stagionali
        float seasonPoints = 0;
        float? finalSeasonPoints;

        float pricePoints = 0;

        string productShoppingListName;

        //quantità presente nel carrello di ogni prodotto richiesto nella lista della spesa
        Dictionary<string, int> cartListQuantities = new Dictionary<string, int>();
        bool listCompleted; //indica se sono stati presi tutti i prodotti della lista della spesa, almeno nella quantità indicata
        
        foreach(string productName in ListaSpesa.listaSpesa.Keys)
        {
            if(!cartListQuantities.ContainsKey(productName))
                cartListQuantities.Add(productName, 0);
        }

        //conto il numero di prodotti nella lista della spesa che possono 
        //soddisfare quality, eco, origin, sustainability e season
        bool firstFound = false;
        foreach(var product in ListaSpesa.listaSpesa)
        {
            if (product.Key.Contains("frutta") || product.Key.Contains("verdura"))
                qualityProducts += product.Value;
            for(int i = 0; i < Loader.productModels.Count && firstFound == false; i++)
            {            
                if (Loader.productModels[i].listName.Contains(product.Key))
                {
                    firstFound = true;
                    if (Loader.productModels[i].packaging.HasValue)
                        ecoProducts += product.Value;
                    if (Loader.productModels[i].origin.HasValue)
                        originProducts += product.Value;
                    if (Loader.productModels[i].sustainable.HasValue)
                        sustainableProducts += product.Value;
                    if (Loader.productModels[i].season != null)
                        seasonProducts += product.Value;
                }
            }
            firstFound = false;
        }

        switch (levelDifficuly)
        {
            case 0: //facile
            //aggiungo punti solo se il prodotto è nella lista della spesa (1),
            //non supera la quantità indicata nella lista (2)
            //e non è scaduto (3) in base a:
            //qualità (4), eco (5) 
            //il prezzo (6) viene valutato sulla spesa totale, non per singolo prodotto
            //(1 + 2 + 3) + 4 + 5 = maxProductPoints, se il prodotto non ha (4) o (5) aggiungo punti di default
                foreach(Product product in  cartList)
                {
                    productShoppingListName = product.model.listName.Split('/')[0].ToLower();
                    if (ListaSpesa.listaSpesa.ContainsKey(productShoppingListName)) //(1)
                    {
                        cartListQuantities[productShoppingListName] += 1;
                        if(cartListQuantities[productShoppingListName] <= ListaSpesa.listaSpesa[productShoppingListName]) //(2)
                        {
                            if(!product.expirated) // (3), se è scaduto il prodotto non dà punti
                            {
                                totalPoints += maxProductPoints / 3; //se non è scaduto aggiungo punti

                                //controllo qualità (4)
                                if (product.model.listName.Contains("frutta") || 
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualità
                                {
                                    if (!product.model.listName.Contains("_old")) //se non è marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 3;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualità
                                {
                                    totalPoints += maxProductPoints / 3;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {                                 
                                    if(product.model.packaging.Value == true) //se è stata presa la versione eco aggiungo punti
                                    {
                                        ecoPoints++;
                                        totalPoints += maxProductPoints / 3;
                                    }
                                }
                                else //caso in cui il prodotto non ha versione eco
                                {
                                    totalPoints += maxProductPoints / 3;
                                }
                            }
                        }
                        else
                        {
                            //tolgo punti per ogni prodotto extra
                            totalPoints -= maxProductPoints / 2;
                        }
                        
                    }
                    else
                    {
                        //tolgo punti per ogni prodotto che non è presente nella lista
                        totalPoints -= maxProductPoints / 2;
                    }
                    
                }

                //controllo prezzo (6)
                listCompleted = cartList.Count > 0 ? true : false;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista è stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista è completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                if (qualityProducts == 0)
                    finalQualityPoints = null;
                else
                    finalQualityPoints = (qualityPoints / qualityProducts) * 100;

                if (ecoProducts == 0)
                    finalEcoPoints = null;
                else
                    finalEcoPoints = (ecoPoints / ecoProducts) * 100;

                return new Result(totalPoints,
                                  finalQualityPoints,
                                  finalEcoPoints,
                                  null,
                                  null,
                                  null,
                                  pricePoints);

            case 1: //normale
                ////(1 + 2 + 3) + 4 + 5 + 6 = maxProductPoints
                foreach (Product product in cartList)
                {
                    productShoppingListName = product.model.listName.Split('/')[0].ToLower();
                    if (ListaSpesa.listaSpesa.ContainsKey(productShoppingListName)) //(1)
                    {
                        cartListQuantities[productShoppingListName] += 1;
                        if (cartListQuantities[productShoppingListName] <= ListaSpesa.listaSpesa[productShoppingListName]) //(2)
                        {
                            if (!product.expirated) // (3), se è scaduto il prodotto non dà punti
                            {
                                totalPoints += maxProductPoints / 4; //se non è scaduto aggiungo punti

                                //controllo qualità (4)
                                if (product.model.listName.Contains("frutta") ||
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualità
                                {                                  
                                    if (!product.model.listName.Contains("_old")) //se non è marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 4;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualità
                                {
                                    totalPoints += maxProductPoints / 4;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {
                                    if (product.model.packaging.Value == true) //se è stata presa la versione eco aggiungo punti
                                    {
                                        ecoPoints++;
                                        totalPoints += maxProductPoints / 4;
                                    }
                                }
                                else //caso in cui il prodotto non ha versione eco
                                {
                                    totalPoints += maxProductPoints / 4;
                                }

                                //controllo origine (6)

                                if (product.model.origin.HasValue) //caso in cui il prodotto ha origine
                                {
                                    originPoints += product.model.origin.Value;
                                    totalPoints += (maxProductPoints / 4) * product.model.origin.Value;                                  
                                }
                                else //caso in cui il prodotto non ha origine
                                {
                                    totalPoints += maxProductPoints / 4;
                                }
                            }
                        }
                        else
                        {
                            //tolgo punti per ogni prodotto extra
                            totalPoints -= maxProductPoints / 2;
                        }
                    }
                    else
                    {
                        //tolgo punti per ogni prodotto che non è presente nella lista
                        totalPoints -= maxProductPoints / 2;
                    }
                }

                //controllo prezzo (7)
                listCompleted = cartList.Count > 0 ? true : false;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista è stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista è completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                if (qualityProducts == 0)
                    finalQualityPoints = null;
                else
                    finalQualityPoints = (qualityPoints / qualityProducts) * 100;

                if (ecoProducts == 0)
                    finalEcoPoints = null;
                else
                    finalEcoPoints = (ecoPoints / ecoProducts) * 100;

                if (originProducts == 0)
                    finalOriginPoints = null;
                else
                    finalOriginPoints = (originPoints / originProducts) * 100;

                return new Result(totalPoints,
                                  finalQualityPoints, 
                                  finalEcoPoints,
                                  finalOriginPoints,
                                  null, 
                                  null,
                                  pricePoints);

            case 2: //difficile
                //(1 + 2 + 3) + 4 + 5 + 6 + 7 + 8 = maxProductPoints
                foreach (Product product in cartList)
                {
                    productShoppingListName = product.model.listName.Split('/')[0].ToLower();
                    if (ListaSpesa.listaSpesa.ContainsKey(productShoppingListName)) //(1)
                    {
                        cartListQuantities[productShoppingListName] += 1;
                        if (cartListQuantities[productShoppingListName] <= ListaSpesa.listaSpesa[productShoppingListName]) //(2)
                        {
                            if (!product.expirated) // (3), se è scaduto il prodotto non dà punti
                            {
                                totalPoints += maxProductPoints / 6; //se non è scaduto aggiungo punti

                                //controllo qualità (4)
                                if (product.model.listName.Contains("frutta") ||
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualità
                                {
                                    if (!product.model.listName.Contains("_old")) //se non è marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 6;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualità
                                {
                                    totalPoints += maxProductPoints / 6;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {
                                    if (product.model.packaging.Value == true) //se è stata presa la versione eco aggiungo punti
                                    {
                                        ecoPoints++;
                                        totalPoints += maxProductPoints / 6;
                                    }
                                }
                                else //caso in cui il prodotto non ha versione eco
                                {
                                    totalPoints += maxProductPoints / 6;
                                }

                                //controllo origine (6)

                                if (product.model.origin.HasValue) //caso in cui il prodotto ha origine
                                {
                                    originPoints += product.model.origin.Value;
                                    totalPoints += (maxProductPoints / 6) * product.model.origin.Value;                                   
                                }
                                else //caso in cui il prodotto non ha origine
                                {
                                    totalPoints += maxProductPoints / 6;
                                }

                                //controllo sustainability (7)

                                if (product.model.sustainable.HasValue) //caso in cui il prodotto ha versione sostenibile
                                {
                                    if(product.model.sustainable.Value == true)
                                    {
                                        sustainablePoints++;
                                        totalPoints += (maxProductPoints / 6) * product.model.origin.Value;
                                    }
                                }
                                else //caso in cui il prodotto non ha versione sostenibile
                                {
                                    totalPoints += maxProductPoints / 6;
                                }

                                //controllo stagione (8)

                                if (product.model.season != null) //caso in cui il prodotto è stagionale
                                {
                                    if (product.model.season[ListaSpesa.season] == 1)
                                    {
                                        seasonPoints++;
                                        totalPoints += (maxProductPoints / 6);
                                    }
                                }
                                else //caso in cui il prodotto non è stagionale
                                {
                                    totalPoints += maxProductPoints / 6;
                                }
                            }
                        }
                        else
                        {
                            //TODO: tolgo punti per ogni prodotto extra
                            totalPoints -= maxProductPoints / 2;
                        }
                    }
                    else
                    {
                        //tolgo punti per ogni prodotto che non è presente nella lista
                        totalPoints -= maxProductPoints / 2;
                    }
                }

                //controllo prezzo (6)
                listCompleted = cartList.Count > 0 ? true : false;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista è stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista è completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                if (qualityProducts == 0)
                    finalQualityPoints = null;
                else
                    finalQualityPoints = (qualityPoints / qualityProducts) * 100;

                if (ecoProducts == 0)
                    finalEcoPoints = null;
                else
                    finalEcoPoints = (ecoPoints / ecoProducts) * 100;

                if (originProducts == 0)
                    finalOriginPoints = null;
                else
                    finalOriginPoints = (originPoints / originProducts) * 100;

                if (sustainableProducts == 0)
                    finalSustainablePoints = null;
                else
                {
                    finalSustainablePoints = (sustainablePoints / sustainableProducts) * 100;
                }

                if (seasonProducts == 0)
                    finalSeasonPoints = null;
                else
                    finalSeasonPoints = (seasonPoints / seasonProducts) * 100;


                return new Result(totalPoints,
                                  finalQualityPoints,
                                  finalEcoPoints,
                                  finalOriginPoints,
                                  finalSustainablePoints,
                                  finalSeasonPoints,
                                  pricePoints);
        }

        return null;
    }

    //questo metodo mappa un valore da un intervallo ad un altro 
    public static float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }

}
