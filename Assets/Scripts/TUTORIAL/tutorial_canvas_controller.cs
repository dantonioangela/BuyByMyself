using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class tutorial_canvas_controller : MonoBehaviour
{
    private Tutorial_typewriter speech;
    public AudioManager audioMan;
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
        audioMan.PlayInstance("00");
    }

    private void Update()
    {
        if ( counter == 0 || counter == 2 || counter == 6 || counter == 13)
        {
            if (Tutorial_typewriter.isReady)
            {
                counter++;
                if (counter == 1) { StopSounds(); audioMan.PlayInstance("01"); }
                else if (counter == 3) { StopSounds(); audioMan.PlayInstance("03");  }
                else if (counter == 7) { StopSounds(); audioMan.PlayInstance("07"); }
                else if (counter == 14) { StopSounds(); audioMan.PlayInstance("14");  }
                speech.ChangeCounter(counter);
                speech.NewSpeech(sentences[counter]);
            }
        }
        if (counter == 16)
        {
            if (Tutorial_typewriter.isReady)
            {
                StopSounds();
                audioMan.PlayInstance("16");

                counter = old_counter;
                speech.ChangeCounter(counter);
                speech.NewSpeech(sentences[counter]);
            }
        }

    }

    public void ChangeSpeech(int c)
    {
        if (c == 2) { StopSounds(); audioMan.PlayInstance("02");}
        if (c == 4) { StopSounds();  audioMan.PlayInstance("04"); }
        if (c == 5) { StopSounds();  audioMan.PlayInstance("05"); }
        if (c == 5) { StopSounds();  audioMan.PlayInstance("05"); }
        if (c == 6) { StopSounds();  audioMan.PlayInstance("06"); }
        if (c == 8) { StopSounds();  audioMan.PlayInstance("08"); }
        if (c == 9) { StopSounds();  audioMan.PlayInstance("09"); }
        if (c == 10) { StopSounds(); audioMan.PlayInstance("10"); }
        if (c == 11) { StopSounds(); audioMan.PlayInstance("11"); }
        if (c == 12) { StopSounds(); audioMan.PlayInstance("12"); }
        if (c == 13) { StopSounds(); audioMan.PlayInstance("13"); }
        if (c == 15) { StopSounds(); audioMan.PlayInstance("15"); }

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

    void StopSounds()
    {
        audioMan.Stop("01");
        audioMan.Stop("02");
        audioMan.Stop("03");
        audioMan.Stop("04");
        audioMan.Stop("05");
        audioMan.Stop("06");
        audioMan.Stop("07");
        audioMan.Stop("08");
        audioMan.Stop("09");
        audioMan.Stop("10");
        audioMan.Stop("11");
        audioMan.Stop("12");
        audioMan.Stop("13");
        audioMan.Stop("14");
        audioMan.Stop("15");
        audioMan.Stop("16");

    }
}
