using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindMysteryAudio : MonoBehaviour
{
    private Object[] objects;
    // Start is called before the first frame update
    void Start()
    {
        objects = Object.FindObjectsOfType<AudioListener>();
        for (int i = 0; i < objects.Length; ++i)
        {
            Debug.Log(objects[i].name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
