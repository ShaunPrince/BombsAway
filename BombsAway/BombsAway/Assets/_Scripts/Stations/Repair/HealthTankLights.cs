using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTankLights : MonoBehaviour
{
    public Material[] materials;
    private Material prevMaterial;
    private float prevIndex;

    public float timeBetweenFlickers;

    public void SetTankLightMaterial(int index)
    {
        if (index > 2 && prevIndex != index)
        {
            this.GetComponent<MaterialTweening>().PingPongMaterial(prevMaterial, materials[3], timeBetweenFlickers);
            prevIndex = index;
        }
        else if (index >= 2 && prevIndex != index)
        {
            this.GetComponent<MaterialTweening>().FlickerMaterial(prevMaterial, materials[2]);
            prevIndex = index;
        }
        else if (prevIndex != index)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(prevMaterial, materials[index]);
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
