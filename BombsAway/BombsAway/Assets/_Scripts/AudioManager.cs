using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] sounds;
    public Sounds[] GettingHit;

    // Start is called before the first frame update
    void Start()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
        foreach (Sounds a in GettingHit)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.loop = a.loop;
        }
    }

    public void PlayArray()
    {
        foreach (Sounds a in GettingHit)
        {
            a.source.Play();
        }

    }
    public void Play(string name)
    {
       Sounds s =  Array.Find(sounds, sound => sound.name == name);
       if (s == null)
        {
            Debug.LogError("Sound " + name + "not found.");
            return;
        }
       s.source.Play();
    }
    // Update is called once per frame
    void Update()
    {

    }
}

