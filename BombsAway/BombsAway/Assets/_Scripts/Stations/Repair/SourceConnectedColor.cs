using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceConnectedColor : MonoBehaviour
{
    public GameObject allTanks;
    //public MaterialTweening tweener;
    public Material connectedMaterial;
    public Material disconnectedMaterial;
    private float flickerTime = 2f;
    private bool currentlyConnected = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (AtLeastOneTankConnected() && !currentlyConnected)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(disconnectedMaterial, connectedMaterial, .2f);
            currentlyConnected = true;
        }
        else if (!AtLeastOneTankConnected() && currentlyConnected)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(connectedMaterial, disconnectedMaterial, .2f);
            currentlyConnected = false;
        }
    }

    public bool AtLeastOneTankConnected()
    {
        foreach (Transform tank in allTanks.transform)
        {
            if (tank.GetComponent<TankController>().isConnectedToSource) return true;
        }

        return false;
    }
}
