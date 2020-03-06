using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTankLights : MonoBehaviour
{
    public GameObject lights;
    public Material[] materials;
    private Material prevMaterial;
    private float prevIndex;

    public float timeBetweenFlickers;

    public void SetTankLightMaterial(int index)
    {
        //if (index > 2 && prevIndex != index)
        //{
        //    lights.GetComponent<MaterialTweening>().PingPongMaterial(prevMaterial, materials[3], timeBetweenFlickers);
        //    prevIndex = index;
        //}
        if (index >= 2 && prevIndex != index)
        {
            lights.GetComponent<MaterialTweening>().FlickerMaterial(materials[2], materials[3]);
            prevIndex = index;
        }
        else if (prevIndex != index)
        {
            lights.GetComponent<MaterialTweening>().MergeMaterial(prevMaterial, materials[index]);
            prevIndex = index;
        }
        prevMaterial = materials[index];
    }

    private void Start()
    {
        prevMaterial = materials[0];
        prevIndex = -1;
    }

}
