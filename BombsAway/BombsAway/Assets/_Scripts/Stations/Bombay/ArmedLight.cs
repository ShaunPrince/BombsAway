using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmedLight : MonoBehaviour
{
    public GameObject armedLight;
    public Material offMaterial;
    public Material onMaterial;
    private ReloadManager reloader;
    private bool armed = false;

    // Start is called before the first frame update
    void Start()
    {
        reloader = this.GetComponent<ReloadManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (armed != !reloader.getReloadingStatus())
        {
            if (!reloader.getReloadingStatus())
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
