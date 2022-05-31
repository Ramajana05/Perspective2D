using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;
    private AudioHotFix audioHotfix;

    /// <summary>
    /// Diese Methode ist dazu da um das Objekt zu löschen falls es null ist und die Liste der Lieder mit allen zu laden
    /// </summary>
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("AudioHotFix"))
        {
            audioHotfix = GameObject.FindGameObjectWithTag("AudioHotFix").GetComponent<AudioHotFix>();
        }
        

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }
    /// <summary>
    /// Diese Methode wird am Anfang aufgerufen
    /// </summary>
    private void Start()
    {
        Play("Theme");
    }
    /// <summary>
    /// Diese Methode ist dazu da um die Musik abzuspielen
    /// </summary>
    /// <param name="name">Name des Musikstuecks</param>
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (GameObject.FindGameObjectWithTag("AudioHotFix"))
            s.source.volume = audioHotfix.musicVolume;
        else
            s.source.volume = 0.2f;
        s.source.Play();

    }
    /// <summary>
    /// Diese Methode ist dazu da um Musik uebereinander abzuspielen
    /// </summary>
    /// <param name="name">Name des Musikstuecks</param>
    public void PlayOneShot(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        if (GameObject.FindGameObjectWithTag("AudioHotFix"))
            s.source.volume = audioHotfix.effectVolume;
        else
            s.source.volume = 0.2f;
        s.source.PlayOneShot(s.source.clip);
    }
    /// <summary>
    /// Diese Methode ist dazu da um die Lautstaerke des Liedes anzupassen
    /// </summary>
    /// <param name="level">Lautstaerke des Liedes</param>
    public void SetEffectVolume(float level)
    {
        audioHotfix.effectVolume = level;
    }

    public void SetMusicVolume(float level)
    {
        audioHotfix.musicVolume = level;
    }
}
