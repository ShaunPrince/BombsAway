using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankStatusColor : MonoBehaviour
{
    public TankController tc;
    public GameObject objectToChange;
    public Material[] materials;
    private Material prevMaterial;
    private float prevFillLevel;

    public float timeBetweenFlickers;

    public void ResetPrevFillLevel()
    {
        prevFillLevel = float.MinValue;
    }
    // Start is called before the first frame update
    void Start()
    {
        prevMaterial = materials[0];
        prevFillLevel = tc.currentFillLevel;
        ChangeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if ( prevFillLevel != tc.currentFillLevel )
        {
            ChangeColor();
        }
    }

    private void ChangeColor()
    {
        // turn green
        if (tc.currentFillLevel == tc.maxFillLevel)
        {
            objectToChange.GetComponent<MaterialTweening>().MergeMaterial(prevMaterial, materials[0]);
            prevMaterial = materials[0];
            prevFillLevel = tc.currentFillLevel;
        }
        // turn red if not connected and below threshold
        else if (tc.currentFillLevel < tc.maxFillLevel - 50 && !tc.isConnectedToSource)
        {
            if (prevMaterial != materials[2])
            {
                objectToChange.GetComponent<MaterialTweening>().FlickerMaterial(prevMaterial, materials[2]);
                prevMaterial = materials[2];
                prevFillLevel = tc.currentFillLevel;
            }
            else if (prevMaterial != materials[3])
            {
                objectToChange.GetComponent<MaterialTweening>().FlickerMaterial(prevMaterial, materials[3]);
                prevMaterial = materials[3];
                prevFillLevel = tc.currentFillLevel;
            }
        }
        // turn yellow if not full
        else if (prevMaterial != materials[1] && tc.currentFillLevel < tc.maxFillLevel)
        {
            objectToChange.GetComponent<MaterialTweening>().MergeMaterial(prevMaterial, materials[1]);
            prevMaterial = materials[1];
            prevFillLevel = tc.currentFillLevel;
        }
    }
}
