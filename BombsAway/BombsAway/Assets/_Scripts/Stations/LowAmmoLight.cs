using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowAmmoLight : MonoBehaviour
{
    [Range(0, 1)]
    public float lowPercentage;
    public GameObject light;
    public Material offMaterial;
    public Material onMaterial;
    //public GameObject lightModel;

    private PlayerGunController controller;
    private bool lightOn = false;
    private bool onStart = true;
    private float time = .6f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponentInChildren<PlayerGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!onStart)
        {
            BlinkLightIfLow();
        }

        else if (onStart && controller.AmmoCount() != 0)
        {
            onStart = false;
        }
    }
    
    private void BlinkLightIfLow()
    {
        float dif = (controller.AmmoCount() / (float)controller.magazineSize);
        //Debug.Log($"{controller.AmmoCount()} / {controller.magazineSize} = {dif}");
        if ( dif < lowPercentage && !lightOn)
        {
            //Debug.Log($"Low ammo");
            //this.GetComponent<MaterialTweening>().PingPongMaterial(offMaterial, onMaterial, time);
            light.GetComponent<MaterialTweening>().FlickerMaterial(offMaterial, onMaterial, time);
            lightOn = true;
        }
        else if (dif > lowPercentage)
        {
            light.GetComponent<MaterialTweening>().MergeMaterial(offMaterial, offMaterial, time);
            lightOn = false;
        }
    }
}
