using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedLight : MonoBehaviour
{
    public GameObject armedLight;
    public Material offMaterial;
    public Material onMaterial;
    public BombBayControls reloader;
    private bool armed = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (armed != !reloader.IsReloading())
        {
            if (!reloader.IsReloading())
            {
                // turn armed light on
                armedLight.GetComponent<Renderer>().material = onMaterial;
                armed = true;
            }
            else
            {
                armedLight.GetComponent<Renderer>().material = offMaterial;
                armed = false;
            }
        }
    }
}
