using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;
    private bool mute = false;
    private float originalVolume;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        if (!mute)
        {
            Play("ThemeMusic");
        }        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            // Toggle mute state for sounds
            foreach (Sound s in sounds)
            {
                s.muted = !s.muted;
                if (s.source != null)
                {
                    s.source.volume = s.muted ? 0f : s.volume;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            // Toggle mute state for music
            mute = !mute;

            foreach (Sound s in sounds)
            {
                if (s.source != null && s.name == "ThemeMusic")
                {
                    s.source.volume = mute ? 0f : s.volume;
                }
            }
        }
    }

    public void Play (string name)
    {
        //Find a sound in the sound array equal to the name
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
}
