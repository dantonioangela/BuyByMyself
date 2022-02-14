using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result
{
    public float totalPoints;
    public float qualityPoints;
    public float ecoPoints; //packaging
    public float? sustainablePoints;
    public float? originPoints;
    public float? seasonPoints;
    public float pricePoints;

    public Result(float totalPoints,
                  float qualityPoints,
                  float ecoPoints,
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
        //tutti i prodotti richiesti nella lista della spesa, almeno nella quantit� indicata
        //quindi non vengono dati se mancano dei prodotti

        float maxPricePoints = 10; //massimo punteggio che si pu� ottenere valutando il prezzo della spesa complessiva
        float maxProductPoints = (100 - maxPricePoints) / ListaSpesa.listaSpesa.Count; //massimo punteggio che un singolo prodotto pu� dare

        float totalPoints = 0;

        int qualityProducts = 0; //numero di prodotti nella lista che hanno il campo qualit� (frutta e verdura)
        float qualityPoints = 0;

        int ecoProducts = 0; //numero di prodotti nella lista che hanno la versione eco
        float ecoPoints = 0;

        int originProducts = 0; //numero di prodotti nella lista che hanno origine
        float originPoints = 0;

        int sustainableProducts = 0; //numero di prodotti nella lista che hanno la versione sostenibile
        float sustainablePoints = 0;

        int seasonProducts = 0; //numero di prodotti nella lista che sono stagionali
        float seasonPoints = 0;

        float pricePoints = 0;

        string productShoppingListName;

        //quantit� presente nel carrello di ogni prodotto richiesto nella lista della spesa
        Dictionary<string, int> cartListQuantities = new Dictionary<string, int>();
        bool listCompleted; //indica se sono stati presi tutti i prodotti della lista della spesa, almeno nella quantit� indicata
        
        foreach(string productName in ListaSpesa.listaSpesa.Keys)
        {
            if(!cartListQuantities.ContainsKey(productName))
                cartListQuantities.Add(productName, 0);
        }

        switch (levelDifficuly)
        {
            case 0: //facile
            //aggiungo punti solo se il prodotto � nella lista della spesa (1),
            //non supera la quantit� indicata nella lista (2)
            //e non � scaduto (3) in base a:
            //qualit� (4), eco (5) 
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
                            if(!product.expirated) // (3), se � scaduto il prodotto non d� punti
                            {
                                totalPoints += maxProductPoints / 3; //se non � scaduto aggiungo punti

                                //controllo qualit� (4)
                                if (product.model.listName.Contains("frutta") || 
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualit�
                                {
                                    qualityProducts++;
                                    if (!product.model.listName.Contains("_old")) //se non � marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 3;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualit�
                                {
                                    totalPoints += maxProductPoints / 3;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {
                                    ecoProducts++;
                                    if(product.model.packaging.Value == true) //se � stata presa la versione eco aggiungo punti
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
                        }
                        
                    }
                    else
                    {
                        //tolgo punti per ogni prodotto che non � presente nella lista
                    }
                }

                //controllo prezzo (6)
                listCompleted = true;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista � stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista � completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                return new Result(totalPoints,
                                  (qualityPoints / qualityProducts) * 100,
                                  (ecoPoints / ecoProducts) * 100,
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
                            if (!product.expirated) // (3), se � scaduto il prodotto non d� punti
                            {
                                totalPoints += maxProductPoints / 4; //se non � scaduto aggiungo punti

                                //controllo qualit� (4)
                                if (product.model.listName.Contains("frutta") ||
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualit�
                                {
                                    qualityProducts++;
                                    if (!product.model.listName.Contains("_old")) //se non � marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 4;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualit�
                                {
                                    totalPoints += maxProductPoints / 4;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {
                                    ecoProducts++;
                                    if (product.model.packaging.Value == true) //se � stata presa la versione eco aggiungo punti
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
                                    originProducts++;
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
                            //tolgo punti per ogni prodotto extra?
                        }
                    }
                    else
                    {
                        //TODO: tolgo punti per ogni prodotto che non � presente nella lista?
                    }
                }

                //controllo prezzo (7)
                listCompleted = true;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista � stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista � completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                return new Result(totalPoints, 
                                  (qualityPoints/qualityProducts)*100, 
                                  (ecoPoints/ ecoProducts) *100, 
                                  (originPoints/ originProducts) *100, 
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
                            if (!product.expirated) // (3), se � scaduto il prodotto non d� punti
                            {
                                totalPoints += maxProductPoints / 6; //se non � scaduto aggiungo punti

                                //controllo qualit� (4)
                                if (product.model.listName.Contains("frutta") ||
                                    product.model.listName.Contains("verdura")) //caso in cui il prodotto ha il campo qualit�
                                {
                                    qualityProducts++;
                                    if (!product.model.listName.Contains("_old")) //se non � marcio aggiungo punti
                                    {
                                        qualityPoints++;
                                        totalPoints += maxProductPoints / 6;
                                    }
                                }
                                else //caso in cui il prodotto non ha campo qualit�
                                {
                                    totalPoints += maxProductPoints / 6;
                                }

                                //controllo eco (5)
                                if (product.model.packaging.HasValue) //caso in cui il prodotto ha versione eco
                                {
                                    ecoProducts++;
                                    if (product.model.packaging.Value == true) //se � stata presa la versione eco aggiungo punti
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
                                    originProducts++;
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
                                    sustainableProducts++;
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

                                if (product.model.season != null) //caso in cui il prodotto � stagionale
                                {
                                    seasonProducts++;
                                    if (product.model.season[ListaSpesa.season] == 1)
                                    {
                                        seasonPoints++;
                                        totalPoints += (maxProductPoints / 6);
                                    }
                                }
                                else //caso in cui il prodotto non ha versione sostenibile
                                {
                                    totalPoints += maxProductPoints / 6;
                                }
                            }
                        }
                        else
                        {
                            //TODO: tolgo punti per ogni prodotto extra?
                        }
                    }
                    else
                    {
                        //TODO: tolgo punti per ogni prodotto che non � presente nella lista?
                    }
                }

                //controllo prezzo (6)
                listCompleted = true;
                foreach (KeyValuePair<string, int> entry in cartListQuantities) //controllo se la lista � stata completata
                {
                    if (entry.Value < ListaSpesa.listaSpesa[entry.Key])
                    {
                        listCompleted = false;
                    }
                }
                //se la lista � completa assumo che Carrello_controller.prezzo_totale_carrello >= ListaSpesa.budgetIdeale
                if (listCompleted)
                {
                    pricePoints = Remap(Carrello_controller.prezzo_totale_carrello, ListaSpesa.idealBudget, ListaSpesa.budget, maxPricePoints, 0);
                    totalPoints += pricePoints;
                    pricePoints *= 10; //per mapparlo su una scala da 0 a 100 invece che da 0 a 10 come ho fatto per tutti gli altri parametri
                }

                return new Result(totalPoints,
                                  (qualityPoints / qualityProducts) * 100,
                                  (ecoPoints / ecoProducts) * 100,
                                  (originPoints / originProducts) * 100,
                                  (sustainablePoints / sustainableProducts) * 100,
                                  (seasonPoints / seasonProducts) * 100,
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
