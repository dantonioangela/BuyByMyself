using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorial_audio : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager.Play("GameplayMusic");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonSound()
    {
        audioManager.PlayInstance("button");
    }

    public void ClickSound()
    {
        audioManager.PlayInstance("click");
    }
}
