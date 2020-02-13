using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMysteryAudio : MonoBehaviour
{
    private Object[] objectsWithAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        objectsWithAudioSource = Object.FindObjectsOfType<AudioSource>();
        for (int i = 0; i < objectsWithAudioSource.Length; ++i)
        {
            Debug.Log(objectsWithAudioSource[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
