using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sounds 
{
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;


    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    [Range(0, 256)]
    public int priority;

    [HideInInspector]
    public AudioSource source;

    public bool loop;
}
