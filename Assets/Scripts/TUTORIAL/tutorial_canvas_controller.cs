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
                                  "Premi M per vedere la mappa del supermercato"};
    // Start is called before the first frame update
    void Start()
    {
        speech = transform.GetChild(1).GetComponent<Tutorial_typewriter>();
        speech.NewSpeech(sentences[0]);
    }

    private void Update()
    {
        if (speech.isReady && counter <2)
        {
            counter++;
            speech.NewSpeech(sentences[counter]);
        }
    }

    public void ChangeSpeech(int c)
    {
        counter = c;
        speech.NewSpeech(sentences[c]);
    }



}
