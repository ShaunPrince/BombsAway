using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowAmmoLight : MonoBehaviour
{
    [Range(0, 1)]
    public float lowPercentage;
    public Material offMaterial;
    public Material onMaterial;
    //public GameObject lightModel;

    private PlayerGunController controller;
    private bool lightOn = false;
    private float time = .6f;

    // Start is called before the first frame update
    void Start()
    {
        controller = this.GetComponentInChildren<PlayerGunController>();
    }

    // Update is called once per frame
    void Update()
    {
        BlinkLightIfLow();
    }
    
    private void BlinkLightIfLow()
    {
        float dif = (controller.AmmoCount() / (float)controller.magazineSize);
        //Debug.Log($"{controller.AmmoCount()} / {controller.magazineSize} = {dif}");
        //if (controller.AmmoCount() == 0)
        //{
        //    this.GetComponent<MaterialTweening>().MergeMaterial(onMaterial, offMaterial, time);
        //    lightOn = false;
        //}
        if ( dif < lowPercentage && !lightOn)
        {
            this.GetComponent<MaterialTweening>().PingPongMaterial(offMaterial, onMaterial, time);
            lightOn = true;
        }
        else if (dif > lowPercentage)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(onMaterial, offMaterial, time);
            lightOn = false;
        }
    }
}
