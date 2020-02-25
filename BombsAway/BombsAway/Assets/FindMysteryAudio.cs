using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMysteryAudio : MonoBehaviour
{
    private AudioSource[] objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = GameObject.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < objects.Length; ++i)
        {
            Debug.Log(objects[i].name);
            Debug.Log(objects[i].clip.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
