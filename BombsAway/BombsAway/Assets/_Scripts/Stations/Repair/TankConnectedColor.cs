using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankConnectedColor : MonoBehaviour
{
    public TankController tc;
    //public MaterialTweening tweener;
    public Material connectedMaterial;
    public Material disconnectedMaterial;
    private float flickerTime = 2f;
    private bool currentlyConnected = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (tc.isConnectedToSource && !currentlyConnected)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(disconnectedMaterial, connectedMaterial);
            currentlyConnected = true;
        }
        else if (!tc.isConnectedToSource && currentlyConnected)
        {
            this.GetComponent<MaterialTweening>().MergeMaterial(connectedMaterial, disconnectedMaterial);
            currentlyConnected = false;
        }
    }
}
