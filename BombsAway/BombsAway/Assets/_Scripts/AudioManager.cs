using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sounds[] General;
    public Sounds[] GettingHit;


    // Start is called before the first frame update

    void Awake()
    {
        foreach(Sounds s in General)
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
    public void Play(string name)
    {
       Sounds s =  Array.Find(General, General => General.name == name);
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

