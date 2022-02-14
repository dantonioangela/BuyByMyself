using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tutorial_canvas_controller : MonoBehaviour
{
    private Tutorial_typewriter speech;
    private bool tutorialStepInventarioDone = false;
    public bool tutorialStepBananaInventarioDone = false;
    private int counter = 0;
    private int old_counter = 0;
    private string[] sentences = {"Ciao! Oggi faremo assieme la tua prima spesa!",
                                  "Iniziamo con il primo compito... Premi L per vedere la lista della spesa",
                                  "Ottimo! Dobbiamo comprare delle banane, vediamo dove si trova il reparto giusto!",
                                  "Premi M per vedere la mappa del supermercato",
                                  "Ora che hai trovato il reparto frutta, prendi due caschi di banane",
                                  "Grande! Adesso prendi anche una bibita",
                                  "Ottima scelta! Manca solo il salmone",
                                  "Clicca sul vetro del reparto frigo per aprirlo",
                                  "Adesso metti il salmone nel carrello!",
                                  "Oops! Abbiamo sforato il budget... Clicca sul carrello per vedere cosa contengono le buste!",
                                  "Quelle banane non sembrano molto buone... Trascinale fuori dalla busta per eliminarle dal carrello!",
                                  "Perfetto, clicca la X per tornare indietro",
                                  "Abbiamo finito la nostra spesa, clicca su di me, alla cassa",
                                  "Super! Hai imparato tutto, ma torna nel tutorial se avrai bisogno di ricordare i comandi",
                                  "Grazie mille e alla prossima, buon divertimento!",
                                  "Grande! Hai scoperto come lasciare il carrello!",
                                  "Per riprenderlo ti basta avvicinarti e cliccarci sopra"};
    // Start is called before the first frame update
    void Start()
    {
        speech = transform.GetChild(1).GetComponent<Tutorial_typewriter>();
        speech.ChangeCounter(0);
        speech.NewSpeech(sentences[0]);
    }

    private void Update()
    {
        if ( counter == 0 || counter == 2 || counter == 6 || counter == 13 || counter == 15)
        {
            if (speech.isReady)
            {
                counter++;
                speech.ChangeCounter(counter);
                speech.NewSpeech(sentences[counter]);
            }
        }
        if (counter == 16)
        {
            if (speech.isReady)
            {
                counter = old_counter;
                speech.ChangeCounter(counter);
                speech.NewSpeech(sentences[counter]);
            }
        }

    }

    public void ChangeSpeech(int c)
    {

        if (c != 12)
        {
            if (c == 15)
            {
                old_counter = counter;
            }
            counter = c;
            speech.ChangeCounter(c);
            speech.NewSpeech(sentences[c]);

            
        }
        else
        {
            if (!tutorialStepInventarioDone && tutorialStepBananaInventarioDone)
            {
                tutorialStepInventarioDone = true;

                counter = c;
                speech.ChangeCounter(c);
                speech.NewSpeech(sentences[c]);
            }
        }
    }
}
