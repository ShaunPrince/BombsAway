using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JunctionConnectedColor : MonoBehaviour
{
    public Junction j;
    public bool connectedToHealthTank;
    public SourceConnectedColor sourceConnection;
    //public MaterialTweening tweener;
    public Material connectedMaterial;
    public Material disconnectedMaterial;
    private bool currentlyConnected = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!connectedToHealthTank)
        {
            if (j.isConnectedToSource && !currentlyConnected)
            {
                this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
                currentlyConnected = true;
            }
            else if (!j.isConnectedToSource && currentlyConnected)
            {
                this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
                currentlyConnected = false;
            }
        }
        else if (connectedToHealthTank) {
            if (sourceConnection.AtLeastOneTankConnected() && !currentlyConnected)
            {
                this.GetComponent<MaterialTweening>().MergeMultipleMaterial(disconnectedMaterial, connectedMaterial, .1f);
                currentlyConnected = true;
            }
            else if (!sourceConnection.AtLeastOneTankConnected() && currentlyConnected)
            {
                this.GetComponent<MaterialTweening>().MergeMultipleMaterial(connectedMaterial, disconnectedMaterial, .1f);
                currentlyConnected = false;
            }
        }
    }
}
