using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_typewriter : MonoBehaviour
{
    private string currentText = "";
    private int i;
    private float delay = 0.08f;
    [System.NonSerialized] public bool isReady = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void NewSpeech( string speech)
    {
        StartCoroutine(ShowText(speech));
    }

    public void Clean()
    {
        this.GetComponent<Text>().text = "";
        currentText = "";
    }

    IEnumerator ShowText( string speech)
    {
        if (isReady)
        {
            isReady = false;
            for (i = 0; i <= speech.Length; i++)
            {
                currentText = speech.Substring(0, i);
                this.GetComponent<Text>().text = currentText;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSecondsRealtime(2);
            Clean();
            isReady = true;
        }

    }
}
