using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private Sound playingSound = null;
    
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        if(playingSound != null)
            playingSound.source.Stop();
        playingSound = s;
        s.source.Play();
    }

    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        playingSound = null;
        s.source.Stop(); 
    }

    public void PlayInstance(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void setGeneralVolume(int volume)
    {
        float value = volume;
        float from1 = 0;
        float to1 = 1;
        float from2 = 0;
        
        foreach(Sound sound in sounds)
        {
            float to2 = sound.volume;
            sound.source.volume = (value - from1) / (to1 - from1) * (to2 - from2) + from2;
        }
    }

}
