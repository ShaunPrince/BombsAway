using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] General;
    public Sounds[] GettingHit;
    public Sounds[] RepairSounds;
    public static AudioManager instance;
    private bool alarmPlaying;
    private float alarmTimer;
    private float alarmTimeLimit;
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
        alarmPlaying = false;
        alarmTimer = 0.0f;
        alarmTimeLimit = 15.0f;
    }


    public void PlayGotHit()
    {
        //Play random shooting sounds
        int randomHit = UnityEngine.Random.Range(1, GettingHit.Length-2);
        Sounds a = GettingHit[randomHit];
        a.source.PlayOneShot(a.source.clip);
    }



    public void PlayMissleHit()
    {
        int missleHit = GettingHit.Length-2;
        Sounds a = GettingHit[missleHit];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayShipDeath()
    {
        int shipDeath = GettingHit.Length - 1;
        Sounds a = GettingHit[shipDeath];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayPipeMove()
    {
        int randomHit = UnityEngine.Random.Range(0, GettingHit.Length);
        Sounds a = RepairSounds[randomHit];
        a.source.PlayOneShot(a.source.clip);

    }

    public void PlayAlarm()
    {
        if (alarmTimer <= alarmTimeLimit)
        {
            int alarm = 0;
            Sounds a = General[alarm];
            a.source.Play();
            alarmPlaying = true;
        }
    }

    public void PlayExplosion()
    {
        int sound = 1;
        Sounds a = General[sound];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayShortClankFX()
    {
        int clank = 2;
        Sounds a = General[clank];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayShortClankMusic()
    {
        int clank = 3;
        Sounds a = General[clank];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayButtonPress()
    {
        int press = 4;
        Sounds a = General[press];
        a.source.PlayOneShot(a.source.clip);
    }

    public void PlayBombDrop()
    {
        int drop = 4;
        Sounds a = General[drop];
        a.source.PlayDelayed(.5f); //.PlayOneShot(a.source.clip);
    }

    public void PlaySqueak1()
    {
        int squeak2 = 6;
        Sounds a = General[squeak2];
        a.source.Stop();

        int squeak1 = 5;
        a = General[squeak1];
        if (a.source.isPlaying == false)
            a.source.Play();
    }

    public void PlaySqueak2()
    {
        int squeak1 = 5;
        Sounds a = General[squeak1];
        a.source.Stop();

        int squeak2 = 6;
        a = General[squeak2];
        if (a.source.isPlaying == false)
            a.source.Play();
    }

    public void StopSquaks()
    {
        General[5].source.Stop();
        General[6].source.Stop();
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
            if (i == 0 && alarmPlaying && alarmTimer <= alarmTimeLimit)
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
        if (alarmPlaying)
        {
            alarmTimer += Time.deltaTime;
            if (alarmTimer >= alarmTimeLimit)
            {
                alarmPlaying = false;
                Stop("General", 0);
            }
        }
    }
}

