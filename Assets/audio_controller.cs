using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audio_controller : MonoBehaviour
{
    public AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        
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
