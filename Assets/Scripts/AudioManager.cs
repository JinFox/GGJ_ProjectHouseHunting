using UnityEngine.Audio;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    Sound currentMusic;

    [SerializeField]
    public Sound[] sounds;


    void Awake()
    {

        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.SetSource(gameObject.AddComponent<AudioSource>());
        }
    }


    public void Play(string name)
    {
        if (!GameSave.Sfx)
            return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
    }


    public void PlayMusic(string name, bool playImmediately = true)
    {
        if (currentMusic != null)
        {
            if (currentMusic.name == name) // don't change
            {
                if (!currentMusic.source.isPlaying && GameSave.Music)
                    currentMusic.source.Play();
                return;
            }
            currentMusic.source.Stop();
            currentMusic.source.time = 0;

        }
        if (!GameSave.Music)
            return;
        //float remainingTime = currentMusic.source.clip.length - currentMusic.source.time;

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
            s.source.Play();
        currentMusic = s;
    }

    public void MusicSettingToggled(bool status, string name)
    {
        if (status)
        {
            PlayMusic(name);
        }
        else
        {
            currentMusic.source.Pause();
        }
    }
}