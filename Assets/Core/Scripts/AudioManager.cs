using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sounds[] clips;
    public static AudioManager instance;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (var sound in clips)
        {
           sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.audio;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
        Play("Theme2");
    }

    public void Play(string name)
    {
       Sounds sound = Array.Find(clips, sound => sound.name == name);
        if (sound == null || sound.source.isPlaying)
            return;
        sound.source.Play();
    }
    public void Stop(string name)
    {
        Sounds sound = Array.Find(clips, sound => sound.name == name);
        if (sound == null)
            return;
        sound.source.Stop();
    }
}
