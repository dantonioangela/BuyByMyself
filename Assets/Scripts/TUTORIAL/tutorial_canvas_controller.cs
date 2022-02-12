using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tutorial_canvas_controller : MonoBehaviour
{
    private Tutorial_typewriter speech;
    private int counter = 0;
    private string[] sentences = {"Ciao! Oggi faremo assieme la tua prima spesa!",
                                  "Iniziamo con il primo compito... Premi L per vedere la lista della spesa",
                                  "Ottimo! Dobbiamo comprare delle banane, vediamo dove si trova il reparto giusto!",
                                  "Premi M per vedere la mappa del supermercato",
                                  "Ora che hai trovato il reparto frutta, prendi due caschi di banane",
                                  "Grande! Adesso compra anche una bibita",
                                  "Ottima scelta! Manca solo il salmone",
                                  "Clicca sul vetro del reparto frigo per aprirlo",
                                  "Adesso compra il salmone!",
                                  "Oops! Abbiamo sforato il budget... Clicca sul carrello per vedere cosa contengono le buste!",
                                  "Quelle banane non sembrano molto buone... Trascinale fuori dalla busta per eliminarle dal carrello!",
                                  "Perfetto, clicca la freccia per tornare indietro",
                                  "Abbiamo finito la nostra spesa, clicca su di me, alla cassa",
                                  "Super! Hai imparato tutto, ma torna nel tutorial se avrai bisogno di ricordare i comandi",
                                  "Grazie mille e alla prossima, buon divertimento!"};
    // Start is called before the first frame update
    void Start()
    {
        speech = transform.GetChild(1).GetComponent<Tutorial_typewriter>();
        speech.ChangeCounter(0);
        speech.NewSpeech(sentences[0]);
    }

    private void Update()
    {
        if (speech.isReady && (counter == 0 || counter == 2 || counter == 6 || counter == 13))
        {
            counter++;
            speech.ChangeCounter(counter);
            speech.NewSpeech(sentences[counter]);
        }
    }

    public void ChangeSpeech(int c)
    {
        counter = c;
        speech.ChangeCounter(c);
        speech.NewSpeech(sentences[c]);
    }
}
