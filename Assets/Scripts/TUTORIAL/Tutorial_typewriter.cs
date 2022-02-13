using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_typewriter : MonoBehaviour
{
    private string currentText = "";
    private int i;
    private float delay = 0.06f;
    public bool stop = false;
    public int counter = 0;
    [System.NonSerialized] public bool isReady = true;
    public RawImage cloud;
    // Start is called before the first frame update
    void Start()
    {
        cloud.enabled = true;
    }

    // Update is called once per frame
    public void NewSpeech( string speech)
    {
        if (!isReady)
        {
            stop = true;
        }
        StartCoroutine(ShowText(speech));
    }

    public void Clean()
    {
        this.GetComponent<Text>().text = "";
        currentText = "";
    }

    IEnumerator ShowText( string speech)
    {
        isReady = false;
        cloud.enabled = true;
        int current_counter = counter;
        for (i = 0; i <= speech.Length; i++)
        {
            cloud.enabled = true;
            currentText = speech.Substring(0, i);
            this.GetComponent<Text>().text = currentText;
            if (counter != current_counter)
            {
                yield break;
            }
            yield return new WaitForSeconds(delay);
        }
        yield return new WaitForSecondsRealtime(2);
        Clean();
        cloud.enabled = false;
        isReady = true;
    }

    public void ChangeCounter(int c)
    {
        counter = c;
    }

}
