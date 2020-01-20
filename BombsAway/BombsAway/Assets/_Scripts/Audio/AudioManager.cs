using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] General;
    public Sounds[] GettingHit;
    public Sounds[] RepairSounds;
    public static AudioManager instance;

    // Start is called before the first frame update

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
        foreach (Sounds s in General)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.priority = s.priority;
            s.source.outputAudioMixerGroup = s.mixerGroup;
        }
        foreach (Sounds a in GettingHit)
        {
            a.source = gameObject.AddComponent<AudioSource>();
            a.source.clip = a.clip;
            a.source.volume = a.volume;
            a.source.loop = a.loop;
            a.source.priority = a.priority;
            a.source.outputAudioMixerGroup = a.mixerGroup;

        }
        foreach (Sounds b in RepairSounds)
        {
            b.source = gameObject.AddComponent<AudioSource>();
            b.source.clip = b.clip;
            b.source.volume = b.volume;
            b.source.loop = b.loop;
            b.source.priority = b.priority;
            b.source.outputAudioMixerGroup = b.mixerGroup;


        }

    }


    public void PlayGotHit()
    {
        //Play random shooting sounds
        for (int i = 0; i < 4; i++)
        {
            int randomHit = UnityEngine.Random.Range(0, GettingHit.Length);
            Sounds a = GettingHit[randomHit];
            a.source.PlayOneShot(a.source.clip);
        }
    }

    public void PlayPipeMove()
    {
        int randomHit = UnityEngine.Random.Range(0, GettingHit.Length);
        Sounds a = RepairSounds[randomHit];
        a.source.PlayOneShot(a.source.clip);

    }

    public void PlayAlarm()
    {
        int alarm = 0;
        Sounds a = General[alarm];
        a.source.PlayOneShot(a.source.clip);

    }

    public void PlayExplosion()
    {
        int sound = 1;
        Sounds a = General[sound];
        a.source.PlayOneShot(a.source.clip);

    }


    //doesnt work.need to fix then delete the top functions
    public void Play(string name, int i)
    {
        Sounds s;
        if (name == "GettingHit")
        {
            s = GettingHit[i];
            s.source.Play();
        }
        else if (name == "General")
        {
            s = General[i];
            s.source.Play();
        }
        else if (name == "RepairSounds")
        {
            s = RepairSounds[i];
            s.source.Play();
        }

    }

    public void Stop(string name, int i)
    {
        Sounds s;
        if (name == "GettingHit")
        {
            s = GettingHit[i];
            s.source.Stop();
        }
        else if (name == "General")
        {
            s = General[i];
            s.source.Stop();
        }
        else if (name == "RepairSounds")
        {
            s = RepairSounds[i];
            s.source.Stop();
        }

    }
   
    void Update()
    {

    }
}

