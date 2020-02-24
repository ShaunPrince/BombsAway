using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthTankLights : MonoBehaviour
{
    public Material[] materials;
    private Material prevMaterial;

    public float timeBetweenFlickers;

    public void SetTankLightMaterial(int index)
    {
        if (index >= 2) this.GetComponent<MaterialTweening>().PingPongMaterial(prevMaterial, materials[3], timeBetweenFlickers);
        else this.GetComponent<MaterialTweening>().MergeMaterial(prevMaterial, materials[index]);
        prevMaterial = materials[index];
    }

    private void Start()
    {
        prevMaterial = materials[0];
    }

}
